using System;

namespace DistributedGrapher.Shared.Core.Models
{
    public abstract class JobQueue<TConfig>
    {
        public int Id { get; protected set; }
        public TConfig Configuration { get; protected set; }
    }
}