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
        public SectionTypes SectionType { get; set; }
    }
}
