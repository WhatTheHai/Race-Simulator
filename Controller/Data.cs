using System;
using Model;
using System.Collections.Generic;
using System.Text;

namespace Controller
{
    public static class Data
    {
        public static Competition Competition { get; set; }

        public static Race CurrentRace { get; set; }

        public static void Initialize()
        {
            Competition = new Competition();
            AddDeelnemers();
            AddTracks();
        }
        public static void AddDeelnemers()
        {
            Car zoef = new Car(10,10,100,false);
            Driver hai = new Driver("Hai", 100, zoef, TeamColors.Green);

            Car minizoef = new Car(10, 10, 100, false);
            Driver gamer = new Driver("Gamer", 80, minizoef, TeamColors.Blue);

            Car megazoef = new Car(9, 9, 90, false);
            Driver speler = new Driver("Speler", 90, megazoef, TeamColors.Red);

            Competition.Participants.Add(hai);
            Competition.Participants.Add(gamer);
            Competition.Participants.Add(speler);
        }
        public static void AddTracks()
        {
            SectionTypes[] sections1 =
            {
                SectionTypes.StartGrid,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.Finish
            };
            Track babyPark = new Track("BabyPark", sections1);
            Competition.Tracks.Enqueue(babyPark);

            SectionTypes[] sections2 =
{
                SectionTypes.StartGrid,
                SectionTypes.LeftCorner,
                SectionTypes.RightCorner,
                SectionTypes.LeftCorner,
                SectionTypes.LeftCorner,
                SectionTypes.RightCorner,
                SectionTypes.LeftCorner,
                SectionTypes.LeftCorner,
                SectionTypes.RightCorner,
                SectionTypes.LeftCorner,
                SectionTypes.Finish
            };
            Track youSpinMeRound = new Track("YouSpinMeRound", sections2);
            Competition.Tracks.Enqueue(youSpinMeRound);
        }

        public static void NextRace()
        {
            Track nextTrack = Competition.NextTrack();
            if(nextTrack != null)
            {
                CurrentRace = new Race(nextTrack, Competition.Participants);
            }
        }
    }
}
