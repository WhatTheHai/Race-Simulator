using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class ParticipantSectionTimes : IDataConstraints
    {
        public string Name { get; set; }
        public Section Section { get; set; }
        public TimeSpan Time { get; set; }
        public void Add(List<IDataConstraints> list)
        {
            foreach (var pst in list.Cast<ParticipantSectionTimes>()
                .Where(pst => pst.Name == Name))
            {
                // overschrijven ?
                pst.Time += Time;
                return;
            }
            list.Add(this);
        }

        public string BestParticipant(List<IDataConstraints> list)
        {
            string name = "";
            TimeSpan Time = new TimeSpan(1,1,1);
            foreach (var pst in list.Cast<ParticipantSectionTimes>()
                .Where(pst => Time > pst.Time))
            {
                Time = pst.Time;
                name = pst.Name;
            }
            return name;
        }
    }
}
