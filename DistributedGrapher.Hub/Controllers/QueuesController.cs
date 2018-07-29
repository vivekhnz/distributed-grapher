using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DistributedGrapher.Shared.Core.Models;
using DistributedGrapher.Shared.Grapher.Models;
using Microsoft.AspNetCore.Mvc;

namespace DistributedGrapher.Hub.Controllers
{
    [Route("api/[controller]")]
    public class QueuesController : Controller
    {
        private readonly IEnumerable<GrapherQueue> queues;

        public QueuesController()
        {
            queues = new List<GrapherQueue>
            {
                new GrapherQueue(1, "x"),
                new GrapherQueue(2, "x+1"),
                new GrapherQueue(3, "2x+3")
            };
        }

        // GET api/queues
        [HttpGet]
        public IEnumerable<GrapherQueue> Get()
        {
            return queues.AsEnumerable();
        }

        // GET api/queues/5
        [HttpGet("{id}")]
        public GrapherQueue Get(int id)
        {
            return queues.FirstOrDefault(q => q.Id == id);
        }
    }
}
