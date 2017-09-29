using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp.Deserializers;

namespace EvidencePlus
{
    public enum Gender { Male, Female }
    public class PersonVM
    {
        public Person person;
        public PersonVM(Person person)
        {
            this.person = person;
        }

        public string Name
        {
            get => person.name;
            set => person.name = value;
        }
        public string Surname
        {
            get => person.surname;
            set => person.surname = value;
        }
        public DateTime BirthDate { get; set; } = DateTime.Now;
        public Gender Gender { get; set; }
        public string BirthNumber1
        {
            get => person.birth_number1;
            set => person.birth_number1 = value;
        }
        
        public string BirthNumber2
        {
            get => person.birth_number2;
            set => person.birth_number2 = value;
        }
    }
}
