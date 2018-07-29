using System;
using DistributedGrapher.Agent.Core;
using DistributedGrapher.Shared.Grapher.Models;

namespace DistributedGrapher.Agent.Grapher
{
    public class GrapherAgent : JobAgent<GrapherQueue, GrapherQueueConfig>
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
    }
}
