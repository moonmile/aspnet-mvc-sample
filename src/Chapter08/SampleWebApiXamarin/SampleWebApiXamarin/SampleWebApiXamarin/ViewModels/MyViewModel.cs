using SampleWebApi.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ClientWPF.ViewModels
{
    class MyViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChange(string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private Person _Person;
        private List<Person> _Persons;
        private List<Perfecture> _Perfectures;
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MyViewModel()
        {
            _Person = new Person();
            _Persons = new List<Person>();
            _Perfectures = new List<Perfecture>();
        }

        /// <summary>
        /// 画面表示のためのPersonプロパティ
        /// </summary>
        public Person Person
        {
            get { return _Person; }
            set
            {
                if (_Person != value)
                {
                    _Person = value;
                    OnPropertyChange("Person");
                    OnPropertyChange("PerfectureIndex");
                }
            }
        }
        /// <summary>
        /// グリッドに表示するPersonリスト
        /// </summary>
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
        /// <summary>
        /// ComboBoxに表示するPerfectureリスト
        /// </summary>
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
        /// <summary>
        /// POSTのためにIDプロパティを付ける
        /// </summary>
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
        /// <summary>
        /// ComboBoxの選択時
        /// </summary>
        public int PerfectureIndex
        {
            get
            {
                if (Perfectures == null)
                    return -1;

                for ( int i=0; i<Perfectures.Count; i++ )
                {
                    if (Perfectures[i].Id == Person.PerfectureId )
                    {
                        return i;
                    }
                }
                return -1;
            }
            set
            {
                if (_Person == null) return;
                if (Perfectures == null) return;

                if (value >= 0)
                {
                    _Person.PerfectureId = Perfectures[value].Id;
                    OnPropertyChange("Person");
                }
            }
        }
    }
}
