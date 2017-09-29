using System;

namespace CrossRoad
{
    using System.Diagnostics;
    using System.Threading;

    class Program
    {
        static void Main(string[] args)
        {
            ////ThreadBasedCrossroadModel.Start();

            ////Stopwatch sw = new Stopwatch();
            ////while (true)
            ////{
            ////    sw.Start();

            ////    Console.WriteLine($"Road 1 {ThreadBasedCrossroadModel.GetRoadState(1)}");
            ////    Console.WriteLine($"Road 2 {ThreadBasedCrossroadModel.GetRoadState(2)}");
            ////    Console.WriteLine($"Road 3 {ThreadBasedCrossroadModel.GetRoadState(3)}");
            ////    Console.WriteLine($"Road 4 {ThreadBasedCrossroadModel.GetRoadState(4)}");

            ////    sw.Stop();
            ////    Console.WriteLine("Elapsed={0}", sw.Elapsed.Seconds);
            ////    Thread.Sleep(1000);
            ////    Console.Clear();
            ////}
            StateMachineBasedCrossRoad.Start();
            Stopwatch sw = new Stopwatch();
            while (true)
            {
                sw.Start();

                Console.WriteLine($"Road 1 {StateMachineBasedCrossRoad.GetRoadState(1)}");
                Console.WriteLine($"Road 2 {StateMachineBasedCrossRoad.GetRoadState(2)}");
                Console.WriteLine($"Road 3 {StateMachineBasedCrossRoad.GetRoadState(3)}");
                Console.WriteLine($"Road 4 {StateMachineBasedCrossRoad.GetRoadState(4)}");

                sw.Stop();
                Console.WriteLine("Elapsed={0}", sw.Elapsed);
                Thread.Sleep(1000);
                sw.Reset();
                Console.Clear();
            }
        }
    }
}
