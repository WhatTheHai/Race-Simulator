using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public interface IDataConstraints
    {
        public string Name { get; set; }
        public void Add(List<IDataConstraints> list);
        public string BestParticipant(List<IDataConstraints> list);
    }
}
