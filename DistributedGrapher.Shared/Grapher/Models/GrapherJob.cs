using System;
using DistributedGrapher.Shared.Core.Models;

namespace DistributedGrapher.Shared.Grapher.Models
{
    public class GrapherJob : Job
    {
        public decimal X;

        public GrapherJob(int id, decimal x)
        {
            Id = id;
            X = x;
        }
    }
}