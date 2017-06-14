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
        private List<Prefecture> _Prefectures;
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MyViewModel()
        {
            _Person = new Person();
            _Persons = new List<Person>();
            _Prefectures = new List<Prefecture>();
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
                    OnPropertyChange("PrefectureIndex");
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
        /// ComboBoxに表示するPrefectureリスト
        /// </summary>
        public List<Prefecture> Prefectures
        {
            get { return _Prefectures; }
            set
            {
                if (_Prefectures != value)
                {
                    _Prefectures = value;
                    OnPropertyChange("Prefectures");
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
        public int PrefectureIndex
        {
            get
            {
                if (Prefectures == null)
                    return -1;

                for ( int i=0; i<Prefectures.Count; i++ )
                {
                    if (Prefectures[i].Id == Person.PrefectureId )
                    {
                        return i;
                    }
                }
                return -1;
            }
            set
            {
                if (_Person == null) return;
                if (Prefectures == null) return;

                if (value >= 0)
                {
                    _Person.PrefectureId = Prefectures[value].Id;
                    OnPropertyChange("Person");
                }
            }
        }
    }
}
