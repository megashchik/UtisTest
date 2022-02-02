using Grpc.Core;
using Model;
using Utis.WorkerIntegration.Server;
using WebApi.Extension;

namespace WebApi.Services
{

    public class WorkerService : WorkerIntegration.WorkerIntegrationBase
    {
        public WorkerService(ITraceableCrud<IPerson> model)
        {
            Model = model;
        }

        ITraceableCrud<IPerson> Model { get; set; }

        public override async Task GetWeatherStream(EmptyMessage request, IServerStreamWriter<WorkerInfoMessage> responseStream, ServerCallContext context)
        {
            foreach (var worker in GetAll())
                await responseStream.WriteAsync(new WorkerInfoMessage()
                {
                    Worker = worker.FromIPerson(),
                    State = State.Update
                });
            await foreach (var info in Model.GetUpdates())
            {
                switch (info.State)
                {
                    case ChangeState.Updated:
                        await responseStream.WriteAsync(new WorkerInfoMessage()
                        {
                            Worker = info.Item.FromIPerson(),
                            State = (State)info.State
                        });
                        break;
                    case ChangeState.Deleted:
                        await responseStream.WriteAsync(new WorkerInfoMessage()
                        {
                            Id = info.Id,
                            State = (State)info.State
                        });
                        break;
                }
            }
        }
        public override Task<EmptyMessage> UpdateWorker(WorkerInfoMessage request, ServerCallContext context)
        {
            switch (request.State)
            {
                case State.Update:
                    Model.AddOrUpdate(request.Worker);
                    break;
                case State.Remove:
                    Model.Delete(request.Worker.Id);
                    break;
                default:
                    throw new InvalidOperationException("Unknown operation");
            }
            return Task.FromResult(new EmptyMessage());
        }
        IEnumerable<IPerson> GetAll()
        {
            int page = 0;
            int pageSize = 100;
            var peopleOnPage = Model.Get(page, pageSize);
            while (peopleOnPage.Length > 0)
            {
                foreach (var person in peopleOnPage)
                    yield return person;
                page++;
                peopleOnPage = Model.Get(page, pageSize);
            }
        }
    }
}
