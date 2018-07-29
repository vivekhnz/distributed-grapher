using System;
using System.Net.Http;
using System.Threading;
using DistributedGrapher.Shared.Core.Models;
using Newtonsoft.Json;

namespace DistributedGrapher.Agent.Core
{
    public abstract class JobAgent<TQueue, TQueueConfig>
        where TQueue : JobQueue<TQueueConfig>
        where TQueueConfig : class
    {
        private static readonly string GetQueueEndpoint = "/api/queues/{0}";

        private readonly string hubBaseUri;
        private readonly int queueId;

        public TimeSpan PollingDelay { get; private set; }

        public JobAgent(string hubBaseUri, int queueId)
        {
            this.hubBaseUri = hubBaseUri;
            this.queueId = queueId;

            PollingDelay = TimeSpan.FromSeconds(1);
        }

        public void Run()
        {
            while (true)
            {
                Console.WriteLine(string.Format("Requesting queue {0} from {1}...", queueId, hubBaseUri));
                var config = GetConfiguration();
                if (config != null)
                {
                    Console.WriteLine("Queue configuration received. Applying configuration...");
                    ApplyConfiguration(config);
                    break;
                }
                Thread.Sleep(PollingDelay);
            }
            Console.WriteLine("Queue configured.");

            while (true)
            {
                Console.WriteLine("Requesting job...");
                Thread.Sleep(PollingDelay);
            }
        }

        private TQueueConfig GetConfiguration()
        {
            var getQueueUri = $"{hubBaseUri}{string.Format(GetQueueEndpoint, queueId)}";
            using (var client = new HttpClient())
            {
                try
                {
                    var request = client.GetAsync(getQueueUri).Result;
                    if (request.IsSuccessStatusCode)
                    {
                        string json = request.Content.ReadAsStringAsync().Result;
                        var queue = JsonConvert.DeserializeObject<TQueue>(json);
                        return queue.Configuration;
                    }
                    return null;
                }
                catch (Exception)
                {
                    // couldn't reach the server
                    return null;
                }
            }
        }

        protected abstract void ApplyConfiguration(TQueueConfig config);
    }
}
