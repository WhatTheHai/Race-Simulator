using System;
using System.Threading;
using Controller;
using Model;


namespace Race_Simulator
{
    class Program
    {
        static void Main(string[] args)
        {
            Data.Initialize();
            Data.NextRace();
            Data.CurrentRace.PlaceAllParticipants();
            //Console.WriteLine(Data.CurrentRace.Track.Name);
            /*            Track tr = Data.CurrentRace.Track;
                        Visualization.setCursorPosition(tr);
                        Console.WriteLine("test");*/

            //Console.WriteLine(tr.Name);
/*            Data.NextRace();
            Data.CurrentRace.PlaceAllParticipants();*/
            Visualization.DrawTrack(Data.CurrentRace.Track);
            for (; ; )
            {
                Thread.Sleep(100);
            }
        }
    }
}
