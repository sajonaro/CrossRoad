using System;
using System.Collections.Generic;
using System.Text;

namespace CrossRoad
{
    using System.Threading;
    using System.Threading.Tasks;

    using Stateless;

    class StateMachineBasedCrossRoad 
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
            switch (stateMachine.State)
            {
                case RoadStates.RoadOneGreenAndTwoRed:
                    if (roadNumber == 1 || roadNumber == 2)
                        return RoadState.Green;
                    else if (roadNumber == 3 || roadNumber == 4)
                        return RoadState.Red;
                    break;

                case RoadStates.RoadOneRedAndTwoGreen:
                    if (roadNumber == 1 || roadNumber == 2)
                        return RoadState.Red;
                    else if (roadNumber == 3 || roadNumber == 4)
                        return RoadState.Green;
                    break;

                case RoadStates.RoadOneYellow:
                    if (roadNumber == 1 || roadNumber == 2)
                        return RoadState.Yellow;
                    else if (roadNumber == 3 || roadNumber == 4)
                        return RoadState.Red;
                    break;
                case RoadStates.RoadTwoYellow:
                    if (roadNumber == 1 || roadNumber == 2)
                        return RoadState.Red;
                    else if (roadNumber == 3 || roadNumber == 4)
                        return RoadState.Yellow;
                    break;
            }
            throw new ArgumentOutOfRangeException(nameof(roadNumber));
        }

        private enum RoadStates
        {
            RoadOneGreenAndTwoRed,
            RoadOneYellow,
            RoadOneRedAndTwoGreen,
            RoadTwoYellow,
        }

        private enum  Triggers
        {
            StartBlinking,
            EndBlinking
        }

        static StateMachine<RoadStates,Triggers> stateMachine = new StateMachine<RoadStates, Triggers>(RoadStates.RoadOneGreenAndTwoRed);

        private static void ConfigureStateMachine()
        {
            stateMachine
                .Configure(RoadStates.RoadOneGreenAndTwoRed)
                .OnEntry(SetTimerForBlinking)
                .Permit(Triggers.StartBlinking, RoadStates.RoadOneYellow);

            stateMachine
                .Configure(RoadStates.RoadOneYellow)
                .OnEntry(SetTimerToEndBlinking)
                .Permit(Triggers.EndBlinking, RoadStates.RoadOneRedAndTwoGreen);
        
            stateMachine
                .Configure(RoadStates.RoadOneRedAndTwoGreen)
                .OnEntry(SetTimerForBlinking)
                .Permit(Triggers.StartBlinking, RoadStates.RoadTwoYellow);

            stateMachine
                .Configure(RoadStates.RoadTwoYellow)
                .OnEntry(SetTimerToEndBlinking)
                .Permit(Triggers.EndBlinking, RoadStates.RoadOneGreenAndTwoRed);


            var dotgraph = stateMachine.ToDotGraph();
        }

        private static async void SetTimerForBlinking()
        {
            //Task.Factory.StartNew(
            //    () => new Timer(o => stateMachine.Fire(Triggers.StartBlinking), null, 4000, Timeout.Infinite))
            //    .ContinueWith(timer => timer.Dispose());

            var timer = Task.Factory.StartNew(() => new Timer(o => stateMachine.Fire(Triggers.StartBlinking), null, 4000, Timeout.Infinite)).Result;
            timer.Dispose();
        }

        private static void SetTimerToEndBlinking()
        {
            Task.Factory.StartNew(
                () => new Timer(o => stateMachine.Fire(Triggers.EndBlinking), null, 1000, Timeout.Infinite))
                .ContinueWith(timer => timer.Dispose());

        }

        public static void Start()
        {

            ConfigureStateMachine();
            stateMachine.Fire(Triggers.StartBlinking);


            //var tr = new Thread(
            //    () =>
            //        {
                      
            //        });

            //tr.Start();

       
        }
    }
}
