using System;

namespace DistributedGrapher.Shared.Core.Models
{
    public abstract class JobQueue<TConfig>
        where TConfig : class
    {
        public int Id;
        public TConfig Configuration;
    }
}