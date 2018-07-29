using System;

namespace DistributedGrapher.Shared.Grapher.Models
{
    public class GrapherQueueConfig
    {
        public string Formula { get; private set; }

        public GrapherQueueConfig(string formula)
        {
            Formula = formula;
        }
    }
}