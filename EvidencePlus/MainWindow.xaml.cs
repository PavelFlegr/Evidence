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

namespace EvidencePlus
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged 
    {
        public ObservableCollection<Person> People { get; set; }
        public Person Current
        {
            get { return _current; }
            set
            {
                _current = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Current)));
            }
        }
        Person _current;
        public MainWindow()
        {
            InitializeComponent();
            People = new ObservableCollection<Person>
            {
                new Person{ Name = "Jméno", Surname = "Příjmení" },
                new Person{ Name = "Jméno", Surname = "Příjmení" }
            };
            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            peopleListBox.SelectedIndex = -1;
            Current = null;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
            {
                Current = new Person((Person)e.AddedItems[0]);
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if(Current == null)
        }
    }
}
