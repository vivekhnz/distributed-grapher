using System;
using System.Threading;
using DistributedGrapher.Agent.Core;
using DistributedGrapher.Shared.Grapher.Models;

namespace DistributedGrapher.Agent.Grapher
{
    public class GrapherAgent : JobAgent<GrapherQueue, GrapherQueueConfig, GrapherJob>
    {
        private string formula;

        public GrapherAgent(string hubBaseUri, int queueId)
            : base(hubBaseUri, queueId)
        {

        }

        protected override void ApplyConfiguration(GrapherQueueConfig config)
        {
            this.formula = config.Formula;
            Console.WriteLine($"Formula: {config.Formula}");
        }

        protected override void RunJob(GrapherJob job)
        {
            Console.WriteLine($"y = {formula.Replace("x", job.X.ToString())}");
            Thread.Sleep(1000);
        }
    }
}
