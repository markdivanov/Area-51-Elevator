using System;
using System.Collections.Generic;
using System.Threading;

namespace Area_51_Elevator
{
    class Program
    {
        static void Main(string[] args)
        {
            Elevator elev = new Elevator();
            List<Thread> Threads = new List<Thread>();
            Random random = new Random();
            for (int i = 1; i <= 10; i++)
            {
                Agent agent = new Agent(i.ToString(), elev);
                int agentNum = random.Next(1, 4);
                agent.Rank = agentNum;

                var thread = new Thread(agent.StartJourney);
                thread.Start();
                Threads.Add(thread);
            }
            foreach (var t in Threads) t.Join();
            Console.WriteLine("Everyone is done with work");
            Console.ReadLine();
        }
    }
}

