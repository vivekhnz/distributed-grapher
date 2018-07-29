﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DistributedGrapher.Shared.Core.Models;
using DistributedGrapher.Shared.Grapher;
using DistributedGrapher.Shared.Grapher.Models;
using Microsoft.AspNetCore.Mvc;

namespace DistributedGrapher.Hub.Controllers
{
    [Route("api/[controller]")]
    public class QueuesController : Controller
    {
        private readonly List<GrapherQueue> queues;
        private readonly JobRepository jobs;

        public QueuesController()
        {
            jobs = new JobRepository();
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

        // GET api/queues/5/next
        [HttpGet("{id}/next")]
        public GrapherJob GetNextJob(int id)
        {
            return jobs.GetNextJob(id);
        }
    }
}
