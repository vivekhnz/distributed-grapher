using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using DistributedGrapher.Shared.Grapher.Models;

namespace DistributedGrapher.Shared.Grapher
{
    public class JobRepository
    {
        private readonly static Dictionary<int, ConcurrentQueue<GrapherJob>> remainingJobs;
        private readonly static Dictionary<int, ConcurrentQueue<GrapherJob>> activeJobs;

        static JobRepository()
        {
            remainingJobs = new Dictionary<int, ConcurrentQueue<GrapherJob>>();
            activeJobs = new Dictionary<int, ConcurrentQueue<GrapherJob>>();
            for (int q = 0; q < 3; q++)
            {
                var jobs = new ConcurrentQueue<GrapherJob>();
                for (int x = 0; x < 100; x++)
                {
                    var job = new GrapherJob(jobs.Count, x);
                    jobs.Enqueue(job);
                }
                remainingJobs.Add(q, jobs);
                activeJobs.Add(q, new ConcurrentQueue<GrapherJob>());
            }
        }

        public GrapherJob GetNextJob(int id)
        {
            if (!remainingJobs.TryGetValue(id, out var remainingQueue)) return null;
            if (!activeJobs.TryGetValue(id, out var activeQueue)) return null;
            if (!remainingQueue.TryDequeue(out var job)) return null;

            activeQueue.Enqueue(job);
            return job;
        }
    }
}