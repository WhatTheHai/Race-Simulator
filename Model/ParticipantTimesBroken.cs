using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class ParticipantTimesBroken : IDataConstraints
    {
        public string Name { get; set; }
        public TimeSpan Time { get; set; }
        public void Add(List<IDataConstraints> list)
        {
            foreach (var ptb in list.Cast<ParticipantTimesBroken>()
                .Where(ptb => ptb.Name == Name))
            {
                ptb.Time += Time;
                return;
            }
            list.Add(this);
        }
        public string BestParticipant(List<IDataConstraints> list)
        {
            TimeSpan Time = new TimeSpan(1,1,1,1);
            string name = "";
            foreach (var ptb in list.Cast<ParticipantTimesBroken>()
                .Where(ptb => Time > ptb.Time))
            {
                Time = ptb.Time;
                name = ptb.Name;
            }
            return name;
        }
    }
}
