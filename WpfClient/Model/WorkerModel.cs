using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utis.WorkerIntegration;

namespace WpfClient.Model
{
    internal class WorkerModel
    {
        public async IAsyncEnumerable<WorkerInfoMessage> GetWorkersAsync()
        {
            ApiClient.WebClient client = new ApiClient.WebClient();
            var enumerator = client.GetAsEnumerable().GetAsyncEnumerator();
            while(await enumerator.MoveNextAsync())
                yield return enumerator.Current;
        }

        public void Update(Worker worker)
        {
            ApiClient.WebClient client = new ApiClient.WebClient();
            client.Update(new WorkerInfoMessage()
            {
                Worker = worker,
                State = State.Update
            });
        }

        public void Delete(Worker worker)
        {
            ApiClient.WebClient client = new ApiClient.WebClient();
            client.Update(new WorkerInfoMessage()
            {
                Worker = worker,
                State = State.Remove
            });
        }
    }
}
