using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class Competition
    {
        public Competition()
        {
            Participants = new List<IParticipant>();
            Tracks = new Queue<Track>();
            ParticipantPoints = new RaceData<ParticipantPoints>();
            ParticipantsTimes = new RaceData<ParticipantSectionTimes>();
            ParticipantTimesBroken = new RaceData<ParticipantTimesBroken>();
            ParticipantSpeedPerTrack = new RaceData<ParticipantSpeedPerTrack>();
        }

        public RaceData<ParticipantPoints> ParticipantPoints { get; set; }
        public RaceData<ParticipantSectionTimes> ParticipantsTimes { get; set; }
        public RaceData<ParticipantTimesBroken> ParticipantTimesBroken { get; set; }
        public RaceData<ParticipantSpeedPerTrack> ParticipantSpeedPerTrack { get; set; }

        public List<IParticipant> Participants { get; set; }
        public Queue<Track> Tracks { get; set; }
        public Track NextTrack()
        {
            return Tracks.Any() ? Tracks.Dequeue() : null;
        }

        public void AddPointsToParticipants(Queue<IParticipant> Participants)
        {
            var maxPoints = Participants.Count * 2;
            var currentPoints = maxPoints + 2;
            while (Participants.Count > 0)
            {
                currentPoints -= 2;
                var currentPart = Participants.Dequeue();
                ParticipantPoints.AddItemToList(new ParticipantPoints()
                {
                    Points = currentPoints, 
                    Name = currentPart.Name
                });
            }
        }

        public void AddSectionTimesToParticipants(string name, Section section, TimeSpan time)
        {
            ParticipantsTimes.AddItemToList(new ParticipantSectionTimes()
            {
                Name = name,
                Section = section,
                Time = time
            });
        }

        public void AddBrokenTime(string name, TimeSpan time)
        {
            ParticipantTimesBroken.AddItemToList(new ParticipantTimesBroken()
            {
                Name = name,
                Time = time
            });
        }

        public void AddParticipantSpeedPerTrack(List<IParticipant> partList, Track track)
        {
            foreach (var participant in partList)
            {
                ParticipantSpeedPerTrack.AddItemToList(new ParticipantSpeedPerTrack()
                {
                    Name = participant.Name,
                    Performance = participant.Equipment.Performance,
                    Speed = participant.Equipment.Speed,
                    Track = track
                });

            }
        }
    }
}
