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
            AddParticipants();
            AddTracks();
            NextRace();
        }
        public static void AddParticipants()
        {
            Car zoef = new Car(10,10,100,false);
            Driver hai = new Driver("Hai", 100, zoef, TeamColors.Green);

            Car minizoef = new Car(10, 9, 100, false);
            Driver gamer = new Driver("Gamer", 80, minizoef, TeamColors.Blue);

            Car megazoef = new Car(9, 9, 90, false);
            Driver speler = new Driver("Speler", 90, megazoef, TeamColors.Red);

            Car ultrazoef = new Car(10, 10, 90, false);
            Driver videospeler = new Driver("Videospeler", 100, ultrazoef, TeamColors.Yellow);

            Car geenzoef = new Car(10, 6, 80, false);
            Driver beginner = new Driver("Beginner", 100, geenzoef, TeamColors.Grey);

            Car superzoef = new Car(9, 8, 80, false);
            Driver lilith = new Driver("Lilith", 100, superzoef, TeamColors.Red);

            Competition.Participants.Add(hai);
            Competition.Participants.Add(gamer);
            Competition.Participants.Add(speler);
            Competition.Participants.Add(videospeler);
            Competition.Participants.Add(beginner);
            Competition.Participants.Add(lilith);
        }
        public static void AddTracks()
        {
            SectionTypes[] sections1 =
            {
                SectionTypes.StartGrid,
                SectionTypes.StartGrid,
                SectionTypes.StartGrid,
                SectionTypes.StartGrid,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Finish,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner
            };
            Track babyPark = new Track("BabyPark", sections1);
            Competition.Tracks.Enqueue(babyPark);

            SectionTypes[] sections2 =
{
                SectionTypes.StartGrid,
                SectionTypes.StartGrid,
                SectionTypes.StartGrid,
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
                SectionTypes.RightCorner,
                SectionTypes.Finish,
                SectionTypes.Straight,
                SectionTypes.LeftCorner,
                SectionTypes.LeftCorner
            };
            Track youSpinMeRound = new Track("YouSpinMeRound", sections2);
            Competition.Tracks.Enqueue(youSpinMeRound);

            Track oostendorp = new Track("Oostendorp", new SectionTypes[] { SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.StartGrid,SectionTypes.Finish, SectionTypes.Straight,
                SectionTypes.LeftCorner, SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.RightCorner,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.RightCorner,SectionTypes.Straight, SectionTypes.RightCorner,
                SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight,
                SectionTypes.LeftCorner,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight, SectionTypes.LeftCorner,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight, SectionTypes.RightCorner,
                SectionTypes.RightCorner, SectionTypes.LeftCorner, SectionTypes.LeftCorner, SectionTypes.RightCorner, SectionTypes.RightCorner,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight,
                SectionTypes.RightCorner,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight, SectionTypes.RightCorner,SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight});
            Competition.Tracks.Enqueue(oostendorp);
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
