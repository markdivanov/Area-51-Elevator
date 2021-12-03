using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Area_51_Elevator
{
    class Agent
    {
        Elevator elev = new Elevator();
        enum OutOfElevator { Walk, EnterElevator, GoHome };

        Random random = new Random();

        public string Name { get; set; }
        public int Rank { get; set; }
        public Elevator Elevator { get; set; }

        private OutOfElevator OutOfElevatorActivity()
        {
            int n = random.Next(10);
            if (n < 3) return OutOfElevator.Walk;
            if (n < 8) return OutOfElevator.EnterElevator;
            return OutOfElevator.GoHome;
        }


        private void WalkOut()
        {
            Console.WriteLine($"{Name} is walking around.");
            Thread.Sleep(100);
        }

        private void EnterElevator(Agent agent)
        {
            Elevator.Enter(this);
            bool InElevator = true;
            while (InElevator)
            {
                elev.CallElevator(this);
                Console.WriteLine($"{Name} with rank {Rank} has pressed a button");
                Thread.Sleep(1000);

                Thread elevThread = new Thread(elev.ChangeFloor);
                elevThread.Start();
                elevThread.Join();
                Thread.Sleep(100);
                bool scan = true;
                while (scan)
                {
                    if (agent.Rank > elev.NewFloor)
                    {
                        scan = false;
                        Console.WriteLine($"{Name} is leaving the elevator to floor" + elev.FloorName);
                        InElevator = false;
                    }
                    else
                    {
                        Console.WriteLine($"Agent does not have the required rank to enter current floor " + elev.FloorName);
                        Thread elevThread1 = new Thread(elev.ChangeFloor);
                        elevThread1.Start();
                        elevThread1.Join();
                    }
                }
            }
            Elevator.Leave(this);
        }

        public void StartJourney()
        {
            WalkOut();
            bool staysOut = true;
            while (staysOut)
            {
                var nextActivity = OutOfElevatorActivity();
                switch (nextActivity)
                {
                    case OutOfElevator.Walk:
                        WalkOut();
                        break;
                    case OutOfElevator.EnterElevator:
                        EnterElevator(this);
                        staysOut = false;
                        break;
                    case OutOfElevator.GoHome:
                        staysOut = false;
                        break;
                    default: throw new NotImplementedException();
                }
            }
            Console.WriteLine($"{Name} is going back home.");
        }

        public Agent(string name, Elevator elevator)
        {
            Name = name;
            Elevator = elevator;
        }
    }
}
