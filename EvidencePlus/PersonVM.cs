using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp.Deserializers;
using System.ComponentModel;

namespace EvidencePlus
{
    public enum Gender { Male, Female }
    public class PersonVM : INotifyPropertyChanged
    {
        public Person person;
        public PersonVM()
        {
            person = new Person();
            BirthDate = DateTime.Now;
            BirthNumber1Index = 0;
        }
        public PersonVM(Person person)
        {
            this.person = person;
            int year = int.Parse(person.birth_number1.Substring(0, 2));
            if (person.birth_number2.Length == 3)
            {
                year += 1900;
            }
            else
            {
                year += 2000; 
            }
            int month = int.Parse(person.birth_number1.Substring(2, 2));
            if (month < 50)
            {
                Gender = 0;
            }
            else
            {
                month -= 50;
                Gender = 1;
            }
            int day = int.Parse(person.birth_number1.Substring(4, 2));

            BirthDate = new DateTime(year, month, day);

            RaisePropertyChanged(nameof(BirthNumber1));
            RaisePropertyChanged(nameof(BirthNumber1Index));
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
        public DateTime BirthDate
        {
            get => _birthDate;
            set
            {
                _birthDate = value;
                RaisePropertyChanged(nameof(BirthNumber1Options));
                BirthNumber1Index = 0;
            }
        }
        DateTime _birthDate;

        void RaisePropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        int _birthNumber1Index;

        public int BirthNumber1Index
        {
            get => _birthNumber1Index;
            set
            {
                _birthNumber1Index = value;
                RaisePropertyChanged(nameof(BirthNumber1Index));
            }
        }

        public List<string> BirthNumber1Options
        {
            get
            {
                return new List<string> { BirthDate.ToString("yy") + (BirthDate.Month + Gender * 50).ToString().PadLeft(2, '0') + BirthDate.ToString("dd") };
            }
        }

        int _gender;

        public int Gender
        {
            get => _gender;
            set
            {
                _gender = value;
                RaisePropertyChanged(nameof(Gender));
                RaisePropertyChanged(nameof(BirthNumber1Options));
                BirthNumber1Index = 0;
            }
        }

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

        public DateTime Today => DateTime.Now;
    }
}
