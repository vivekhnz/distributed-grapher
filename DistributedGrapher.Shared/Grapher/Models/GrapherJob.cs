using System;
using DistributedGrapher.Shared.Core.Models;

namespace DistributedGrapher.Shared.Grapher.Models
{
    public class GrapherJob : Job
    {
        public int X;

        public GrapherJob(int id, int x)
        {
            Id = id;
            X = x;
        }
    }
}