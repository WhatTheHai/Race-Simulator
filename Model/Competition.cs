using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class Competition
    {
        List<IParticipant> Participants { get; set; }
        Queue<Track> Tracks { get; set; }
        public Track NextTrack()
        {
            return null;
        }
    }
}
