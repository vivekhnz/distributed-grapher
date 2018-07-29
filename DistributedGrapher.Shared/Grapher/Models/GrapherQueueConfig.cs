using System;

namespace DistributedGrapher.Shared.Grapher.Models
{
    public class GrapherQueueConfig
    {
        public string Formula;

        public GrapherQueueConfig(string formula)
        {
            Formula = formula;
        }
    }
}