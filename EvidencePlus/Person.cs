using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidencePlus
{
    public class Person
    {
        public Person() { }
        public Person(Person person)
        {
            Name = person.Name;
            Surname = person.Surname;
        }
        public string Name { get; set; }
        public string Surname { get; set; }

        public override bool Equals(object obj)
        {
            Person person;
            if ((person = obj as Person) != null)
            {

                return
                    Name == person.Name &&
                    Surname == person.Surname;
            }
            
            return base.Equals(obj);
        }
    }
}
