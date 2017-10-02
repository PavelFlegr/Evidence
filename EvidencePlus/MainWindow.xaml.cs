using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.ComponentModel;
using RestSharp;

namespace EvidencePlus
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public DateTime Today => DateTime.Now;
        public List<PersonVM> People { get; set; }
        public int CurrentIndex
        {
            get => _currentIndex;
            set
            {
                _currentIndex = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentIndex)));
            }
        }
        int _currentIndex = -1;

        public PersonVM Current
        {
            get => _current;
            set
            {
                _current = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Current)));
            }
        }

        public List<PersonVM> GetPeople()
        {
            var client = new RestClient("https://student.sps-prosek.cz/~flegrpa14/evidence/");
            var req = new RestRequest(Method.GET);
            var res = client.Execute<List<Person>>(req);
            var people = res.Data;
            if (people == null)
            {
                return new List<PersonVM>();
            }
            return people.Select(person => new PersonVM(person)).ToList();
        }

        PersonVM _current = new PersonVM();
        public MainWindow()
        {
            InitializeComponent();

            People = GetPeople();
            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
            {
                Current = new PersonVM(((PersonVM)e.AddedItems[0]).person);
            }
            else
            {
                Current = new PersonVM();
            }
        }

        void SavePerson(Person person)
        {
            var client = new RestClient("https://student.sps-prosek.cz/~flegrpa14/evidence/index.php/" + (CurrentIndex != -1 ? person.id.ToString() : ""));
            var req = new RestRequest(CurrentIndex == -1 ? Method.POST : Method.PUT);
            req.AddParameter("birth_number1", person.birth_number1);
            req.AddParameter("birth_number2", person.birth_number2);
            req.AddParameter("name", person.name);
            req.AddParameter("surname", person.surname);

            var res = client.Execute(req);
            People = GetPeople();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(People)));
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (Current.person.birth_number2 != null && Current.person.birth_number1 != null)
            {
                if ((Current.BirthDate.Year >= 1954 && Current.person.birth_number2.Length == 4) ||
                    (Current.BirthDate.Year < 1954 && Current.person.birth_number2.Length == 3))
                {
                    SavePerson(Current.person);
                    return;
                }
            }

            MessageBox.Show("Neplatné rodné číslo");
            return;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var person = (PersonVM)button.DataContext;
            var client = new RestClient("https://student.sps-prosek.cz/~flegrpa14/evidence/index.php/"+ person.person.id);
            var req = new RestRequest(Method.DELETE);

            var res = client.Execute(req);
            People = GetPeople();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(People)));
        }
    }
}
