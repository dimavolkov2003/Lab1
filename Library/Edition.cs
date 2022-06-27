using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Edition
    {
        public string ID { get; private set; }
        public string Name { get; private set; }
        public string Location { get; private set; }
        public Edition(string ID, string name, string location)
        {
            this.ID = ID;
            Name = name;
            Location = location;
        }
    }
}
