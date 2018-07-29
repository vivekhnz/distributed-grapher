using System;
using DistributedGrapher.Shared.Core.Models;

namespace DistributedGrapher.Shared.Grapher.Models
{
    public class GrapherQueue : JobQueue<GrapherQueueConfig>
    {
        public GrapherQueue(int id, string formula)
        {
            Id = id;
            Configuration = new GrapherQueueConfig(formula);
        }
    }
}