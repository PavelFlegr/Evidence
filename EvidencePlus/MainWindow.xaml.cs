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

        public int GenderIndex
        {
            get => (int)Current.Gender;
            set => Current.Gender = (Gender)value;
        }

        public PersonVM Current
        {
            get => _current;
            set
            {
                _current = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Current)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(GenderIndex)));
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

        PersonVM _current = new PersonVM(new Person());
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
                Current = new PersonVM(new Person());
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(GenderIndex)));

            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentIndex == -1)
            {
                People.Add(Current);
                CurrentIndex = People.Count - 1;
            }
            else
            {
                var i = CurrentIndex;
                People[CurrentIndex] = Current;
                CurrentIndex = i;
            }
        }
    }
}
