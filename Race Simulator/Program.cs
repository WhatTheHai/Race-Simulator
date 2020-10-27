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
            Visualization.Initialize();
            Visualization.DrawTrack(Data.CurrentRace.Track);
            for (; ; )
            {
                Thread.Sleep(100);
            }
        }
    }
}
