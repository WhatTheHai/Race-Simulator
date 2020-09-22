using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Track
    {
        string Name { get; set; }
        LinkedList<Section> Sections { get; set; }
        public Track(string name, SectionTypes[] sections)
        {
            Name = name;
            Sections = new LinkedList<Section>();
        }
    }
}
