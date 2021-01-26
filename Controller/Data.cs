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
            Car zoef = new Car(10,100,10,false);
            Driver hai = new Driver("Hai", 0, zoef, TeamColors.Green);

            Car minizoef = new Car(10, 90, 10, false);
            Driver gamer = new Driver("Gamer", 0, minizoef, TeamColors.Blue);

            Car megazoef = new Car(9, 90, 10, false);
            Driver speler = new Driver("Speler", 0, megazoef, TeamColors.Red);

            Car ultrazoef = new Car(10, 100, 10, false);
            Driver videospeler = new Driver("Videospeler", 0, ultrazoef, TeamColors.Yellow);

            Car geenzoef = new Car(10, 70, 10, false);
            Driver beginner = new Driver("Beginner", 0, geenzoef, TeamColors.Grey);

            Car superzoef = new Car(9, 80, 10, false);
            Driver lilith = new Driver("Lilith", 0, superzoef, TeamColors.Purple);

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
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Finish,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner
            };
            /*Track babyPark = new Track("BabyPark", sections1);
            Competition.Tracks.Enqueue(babyPark);*/

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
            /*Track youSpinMeRound = new Track("YouSpinMeRound", sections2);
            Competition.Tracks.Enqueue(youSpinMeRound);*/

            SectionTypes[] sections3 =
            {
                SectionTypes.Straight,
                SectionTypes.StartGrid,
                SectionTypes.StartGrid,
                SectionTypes.StartGrid,
                SectionTypes.StartGrid,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Finish,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner
            };
            Track castleRun = new Track("CastleRun", sections3);
            Competition.Tracks.Enqueue(castleRun);


            /*Track oostendorp = new Track("Oostendorp", new SectionTypes[] { SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.StartGrid,SectionTypes.Finish, SectionTypes.Straight,
                SectionTypes.LeftCorner, SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.RightCorner,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.RightCorner,SectionTypes.Straight, SectionTypes.RightCorner,
                SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight,
                SectionTypes.LeftCorner,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight, SectionTypes.LeftCorner,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight, SectionTypes.RightCorner,
                SectionTypes.RightCorner, SectionTypes.LeftCorner, SectionTypes.LeftCorner, SectionTypes.RightCorner, SectionTypes.RightCorner,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight,
                SectionTypes.RightCorner,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight, SectionTypes.RightCorner,SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight});
            Competition.Tracks.Enqueue(oostendorp);*/
        }

        public static void NextRace()
        {
            Track nextTrack = Competition.NextTrack();
            if(nextTrack != null)
            {
                CurrentRace = new Race(nextTrack, Competition.Participants);
                CurrentRace.NextRace += OnNextRace;
            }
            else
            {
                CurrentRace = null;
            }
        }

        public static void OnNextRace(object sender, EventArgs e)
        {
            Competition.AddPointsToParticipants(CurrentRace.finalScore);
            Competition.AddParticipantSpeedPerTrack(CurrentRace.Participants, CurrentRace.Track);
        }
    }
}
