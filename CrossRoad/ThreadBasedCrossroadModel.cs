using System;

namespace CrossRoad
{
    using System.Threading;

    /// <summary>
    /// represents traffic lights system on a 2 road intersection
    /// </summary>
    class ThreadBasedCrossroadModel 
    {
        /// <summary>
        ///  
        /// road directions  are denoted as follows:
        /// 
        ///            | 1    |
        ///         ---        -----
        ///                    4
        ///           3         
        ///         ---        -----
        ///            |    2 |
        /// 
        ///  
        /// </summary>
        /// <param name="roadNumber"></param>
        /// <returns>status of the traffic ligh (road status) for a given direction</returns>
        public static RoadState GetRoadState(int roadNumber)
        {

            if (roadNumber == 1 || roadNumber == 2)
                return roadBState;

            if (roadNumber == 3 || roadNumber == 4)
                return roadAState;

            throw new ArgumentOutOfRangeException(nameof(roadNumber),roadNumber,"road number has to be in the range 1-4 ");
        }

        private static RoadState roadAState;
        private static RoadState roadBState;

        private const int fullPeriod = 5000;
        private const int subPeriod = 1000;

        public static void Start()
        {
            Thread tr = new Thread(() =>
            {
                while (true)
                {
                    roadBState = RoadState.Green;
                    roadAState = RoadState.Red;
                    Thread.Sleep(fullPeriod - subPeriod);
                    roadBState = RoadState.Yellow;
                    Console.WriteLine("change to yellow");
                    Thread.Sleep(subPeriod);
                    roadBState = RoadState.Red;
                    roadAState = RoadState.Green;
                    Thread.Sleep(fullPeriod - subPeriod);
                    roadAState = RoadState.Yellow;
                    Console.WriteLine("change to yellow");
                    Thread.Sleep(subPeriod);

                }

            });

            tr.Start();
        }

    }
}
