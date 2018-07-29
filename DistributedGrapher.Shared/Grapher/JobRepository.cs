using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using DistributedGrapher.Shared.Grapher.Models;

namespace DistributedGrapher.Shared.Grapher
{
    public class JobRepository
    {
        public class Queue
        {
            public ConcurrentDictionary<int, GrapherJob> Jobs;
            public ConcurrentQueue<int> RemainingJobs;
            public ConcurrentDictionary<int, int> ActiveJobs;
            public ConcurrentDictionary<int, decimal> JobResults;

            public Queue()
            {
                Jobs = new ConcurrentDictionary<int, GrapherJob>();
                RemainingJobs = new ConcurrentQueue<int>();
                ActiveJobs = new ConcurrentDictionary<int, int>();
                JobResults = new ConcurrentDictionary<int, decimal>();
            }

            public void AddJob(GrapherJob job)
            {
                Jobs.TryAdd(job.Id, job);
                RemainingJobs.Enqueue(job.Id);
            }

            public GrapherJob Dequeue()
            {
                if (!RemainingJobs.TryDequeue(out int id)) return null;
                if (!Jobs.TryGetValue(id, out var job)) return null;

                ActiveJobs.TryAdd(id, id);
                return job;
            }

            public void SaveResult(int jobId, decimal result)
            {
                ActiveJobs.TryRemove(jobId, out var _);
                JobResults.TryAdd(jobId, result);
            }

            public Dictionary<decimal, decimal> GetResults()
            {
                var results = new Dictionary<decimal, decimal>();

                foreach (var result in JobResults)
                {
                    if (!Jobs.TryGetValue(result.Key, out var job)) continue;
                    results.Add(job.X, result.Value);
                }

                return results;
            }
        }

        private readonly static ConcurrentDictionary<int, Queue> queues;

        static JobRepository()
        {
            queues = new ConcurrentDictionary<int, Queue>();
            for (int q = 0; q < 3; q++)
            {
                var queue = new Queue();
                for (int x = 0; x < 100; x++)
                {
                    queue.AddJob(new GrapherJob(x, x));
                }
                queues.TryAdd(q, queue);
            }
        }

        public GrapherJob GetNextJob(int queueId)
        {
            if (!queues.TryGetValue(queueId, out var queue)) return null;
            return queue.Dequeue();
        }

        public void SaveResult(int queueId, int jobId, decimal result)
        {
            if (!queues.TryGetValue(queueId, out var queue)) return;
            queue.SaveResult(jobId, result);
        }

        public Dictionary<decimal, decimal> GetResults(int queueId)
        {
            if (!queues.TryGetValue(queueId, out var queue)) return null;
            return queue.GetResults();
        }
    }
}