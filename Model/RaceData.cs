using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class RaceData<T> where T: IDataConstraints
    {
        private List<IDataConstraints> _list = new List<IDataConstraints>();

        public void AddItemToList(T item)
        {
            item.Add(_list);
        }

        public string bestParticipant()
        {
            return _list.Any() ? _list[0].BestParticipant(_list) : "To be determined";
        }
    }
}
