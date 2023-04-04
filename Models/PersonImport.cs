using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{

    public class PersonsImport
    {
        List<PersonWithAddress>? Persons{ get; set; }
    }

    public class PersonWithAddress :Person
    {
        public List<Address>? address { get; set; }
    }
}
