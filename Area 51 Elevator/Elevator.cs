using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Area_51_Elevator
{
    class Elevator
    {
        public string FloorName { get; set; }
        public int NewFloor { get; set; }

        Random random = new Random();
        List<Agent> agents = new List<Agent>();
        Semaphore door = new Semaphore(1, 1);

        public void Enter(Agent agent)
        {
            door.WaitOne();
            lock (agents)
            {
                agents.Add(agent);
            }
        }

        public void Leave(Agent agent)
        {
            lock (agents)
            {
                agents.Remove(agent);
            }
            door.Release();
        }
        public void CallElevator(Agent agent)
        {
            Console.WriteLine($"{agent.Name} is calling the elevator.");
            Thread.Sleep(2000);
            Console.WriteLine($"Elevator has arrived.");
            Thread.Sleep(1000);
            Console.WriteLine($"{agent.Name} entered the elevator");

        }
        public void ChangeFloor()
        {
            int n = random.Next(4);
            if (n == 1)
            {
                Console.WriteLine($"The elevator is going to floor G");
                NewFloor = 0;
                FloorName = "G";
            }
            if (n == 2)
            {
                Console.WriteLine($"The elevator is going to floor S");
                NewFloor = 1;
                FloorName = "S";
            }
            if (n == 3)
            {
                Console.WriteLine($"The elevator is going to floor T1");
                NewFloor = 2;
                FloorName = "T1";
            }
            if (n == 4)
            {
                Console.WriteLine($"The elevator is going to floor T2");
                NewFloor = 2;
                FloorName = "T2";
            }
        }
    }
}

