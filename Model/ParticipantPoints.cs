using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class ParticipantPoints : IDataConstraints

    {
        public string Name { get; set; }
        public int Points { get; set; }
        public void Add(List<IDataConstraints> list)
        {
            foreach (var pp in list.Cast<ParticipantPoints>()
                .Where(pp => pp.Name == Name))
            {
                pp.Points += Points;
                return;
            }
            list.Add(this);
        }
        public string BestParticipant(List<IDataConstraints> list)
        {
            int highestScore = 0;
            string name = "";
            foreach (var p in list.Cast<ParticipantPoints>()
                .Where(p => p.Points > highestScore))
            {
                highestScore = p.Points;
                name = p.Name;
            }
            return name;
        }
    }
}
