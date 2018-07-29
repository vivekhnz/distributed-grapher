using System;
using System.Threading;
using DistributedGrapher.Agent.Core;
using DistributedGrapher.Shared.Grapher.Models;
using SimpleExpressionEvaluator;

namespace DistributedGrapher.Agent.Grapher
{
    public class GrapherAgent : JobAgent<GrapherQueue, GrapherQueueConfig, GrapherJob, decimal>
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

        protected override decimal RunJob(GrapherJob job)
        {
            // evaluate formula
            dynamic engine = new ExpressionEvaluator();
            decimal y = engine.Evaluate(formula, x: job.X);
            Thread.Sleep(1000);

            return y;
        }
    }
}
