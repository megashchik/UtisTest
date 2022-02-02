using Grpc.Net.Client;
using Utis.WorkerIntegration;

namespace ApiClient
{
    public class WebClient
    {
        string address = "https://localhost:7076";
        WorkerIntegration.WorkerIntegrationClient GetClient()
            => new WorkerIntegration.WorkerIntegrationClient(GrpcChannel.ForAddress(address));


        public async IAsyncEnumerable<WorkerInfoMessage> GetAsEnumerable()
        {
            var client = GetClient();
            var stream = client.GetWeatherStream(new EmptyMessage()).ResponseStream;

            var token = new CancellationTokenSource().Token;
            while(await stream.MoveNext(token))
                yield return stream.Current;
        }

        public void Update(WorkerInfoMessage message)
        {
            var client = GetClient();
            client.UpdateWorker(message);
        }

    }
}