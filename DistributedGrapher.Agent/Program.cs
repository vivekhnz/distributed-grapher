using System;
using DistributedGrapher.Agent.Grapher;

namespace DistributedGrapher.Agent
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Starting agent...");

            var agent = new GrapherAgent("http://localhost:5000", 1);
            agent.Run();
        }
    }
}
