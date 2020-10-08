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
            //Console.WriteLine(Data.CurrentRace.Track.Name);
/*            Track tr = Data.CurrentRace.Track;
            Visualization.setCursorPosition(tr);
            Console.WriteLine("test");*/

            Track tr = Data.Competition.NextTrack();
            Console.WriteLine(tr.Name);
            Visualization.setCursorPosition(tr);
            Console.WriteLine("test");
            for (; ; )
            {
                Thread.Sleep(100);
            }
        }
    }
}
