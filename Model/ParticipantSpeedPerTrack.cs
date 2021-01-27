using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Model
{
    public class ParticipantSpeedPerTrack : IDataConstraints
    {
        public string Name { get; set; }
        public int Performance { get; set; }
        public int Speed { get; set; }
        public Track Track { get; set; }
        public void Add(List<IDataConstraints> list)
        {
            //No need for grouping, only adds after every Track.
            list.Add(this);
        }

        public string BestParticipant(List<IDataConstraints> list)
        {
            int fastestTotal = 0;
            string trackName = "";
            string name = "";
            foreach (var pspt in list.Cast<ParticipantSpeedPerTrack>()
                .Where(pspt => pspt.Performance * pspt.Speed > fastestTotal))
            {
                fastestTotal = pspt.Performance * pspt.Speed;
                trackName = pspt.Track.Name;
                name = pspt.Name;
            }
            string nameAndTrack = $"{name} in the track {trackName}"; 
            return nameAndTrack;
        }
    }
}
