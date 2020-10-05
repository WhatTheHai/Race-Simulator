using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Section
    {
        public Section(SectionTypes section)
        {
            SectionType = section;
        }
        //Constuctor gemaakt voor Track.cs, anders moet je de constructor in de functie zelf doen.
        public SectionTypes SectionType { get; set; }
    }
}
