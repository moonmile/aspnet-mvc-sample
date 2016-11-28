using SampleWebApi.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ClientUWP.ViewModels
{
    class MyViewModel : INotifyPropertyChanged
    {
        private Person _Person;
        private List<Person> _Persons;
        private List<Perfecture> _Perfectures;

        public MyViewModel()
        {
            _Person = new Person();
            _Persons = new List<Person>();
            _Perfectures = new List<Perfecture>();
        }

        public Person Person
        {
            get { return _Person; }
            set
            {
                if (_Person != value)
                {
                    _Person = value;
                    OnPropertyChange("Person");
                }
            }
        }
        public List<Person> Persons
        {
            get { return _Persons; }
            set
            {
                if (_Persons != value)
                {
                    _Persons = value;
                    OnPropertyChange("Persons");
                }
            }
        }
        public List<Perfecture> Perfectures
        {
            get { return _Perfectures; }
            set
            {
                if (_Perfectures != value)
                {
                    _Perfectures = value;
                    OnPropertyChange("Perfectures");
                }
            }
        }

        public int ID
        {
            get { return _Person.Id; }
            set {  if ( _Person.Id != value )
                {
                    _Person.Id = value;
                    OnPropertyChange("Person");
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChange(string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
