using Repository;
using System.Collections.Concurrent;


namespace Model
{
    public class WorkerModel : ITraceableCrud<IPerson>
    {
        public static event Action<ChangeCommand> WasChanged = null!;
        WorkersRepository Repository { get; } = new PostgreSqlFactory().GetWorkersRepository();

        AsyncQueue<ChangeCommand> UpdatedWorkers { get; } = new AsyncQueue<ChangeCommand>();

        public WorkerModel()
        {
            WasChanged += Enqueue; 
        }

        void Enqueue(ChangeCommand command)
        {
            UpdatedWorkers.Enqueue(command);
        }

        public int AddOrUpdate(IPerson entity)
        {
            int id = Repository.AddOrUpdate(entity);
            WasChanged.Invoke(new ChangeCommand(id, ChangeState.Updated));
            return id;
        }

        public void Delete(int id)
        {
            Repository.Delete(id);
            WasChanged.Invoke(new ChangeCommand(id, ChangeState.Deleted));
        }

        public IPerson[] Get(int page, int pageSize)
        {
            return Repository.Get(page, pageSize);
        }

        public IPerson Get(int id)
        {
            return Repository.Get(id);
        }

        public async IAsyncEnumerable<IInfo<IPerson>> GetUpdates()
        {
            await foreach(ChangeCommand command in UpdatedWorkers)
            {
                switch(command.State)
                {
                    case ChangeState.Updated:
                        yield return new WorkerInfo { Item = Get(command.Id), State = ChangeState.Updated };
                        break;
                    case ChangeState.Deleted:
                        yield return new WorkerInfo() { Id = command.Id, State = ChangeState.Deleted };
                        break;
                }
            }
        }
    }
}