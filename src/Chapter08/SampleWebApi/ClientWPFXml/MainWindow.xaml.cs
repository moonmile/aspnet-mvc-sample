using ClientUWP.ViewModels;
using SampleWebApiXml.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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

namespace ClientWPFXml
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _vm = new MyViewModel();
            this.DataContext = _vm;

            // 都道府県データを読み込む
            var hc = new HttpClient();
            hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
            var res = await hc.GetAsync("http://localhost:5000/api/Prefectures");
            var st = await res.Content.ReadAsStreamAsync();
            var xs = new System.Xml.Serialization.XmlSerializer(typeof(Prefectures));
            var obj = xs.Deserialize(st) as Prefectures;
            _vm.Prefectures = obj.Items;
        }
        MyViewModel _vm;


        private async void clickGet(object sender, RoutedEventArgs e)
        {
            var hc = new HttpClient();
            hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
            var res = await hc.GetAsync("http://localhost:5000/api/People");
            var st = await res.Content.ReadAsStreamAsync();
            var xs = new System.Xml.Serialization.XmlSerializer(typeof(People));
            var obj = xs.Deserialize(st) as People;
            _vm.Persons = obj.Items;
        }

        private async void clickGetId(object sender, RoutedEventArgs e)
        {
            if (_vm.Person.Id == 0) return;
            int id = _vm.Person.Id;

            var hc = new HttpClient();
            hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
            var res = await hc.GetAsync($"http://localhost:5000/api/People/{id}");
            var st = await res.Content.ReadAsStreamAsync();
            var xs = new System.Xml.Serialization.XmlSerializer(typeof(Person));
            var item = xs.Deserialize(st) as Person;
            _vm.Person = item;

        }

        private async void clickPost(object sender, RoutedEventArgs e)
        {
            var hc = new HttpClient();
            _vm.Person.Id = 0;
            var xs = new System.Xml.Serialization.XmlSerializer(typeof(Person));
            hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
            var sw = new System.IO.StringWriter();
            // 先頭の <?xml ... をカットする
            var settings = new System.Xml.XmlWriterSettings() { OmitXmlDeclaration = true, Encoding = Encoding.UTF8 };
            var xw = System.Xml.XmlWriter.Create(sw, settings);
            xs.Serialize(xw, _vm.Person);
            var xml = sw.ToString();
            var cont = new StringContent(xml, Encoding.UTF8, "application/xml");
            var res = await hc.PostAsync("http://localhost:5000/api/People", cont);
            var st = await res.Content.ReadAsStreamAsync();
            var xs2 = new System.Xml.Serialization.XmlSerializer(typeof(int));
            int id = (int)xs2.Deserialize(st);
            _vm.ID = id;
        }

        private async void clickPutId(object sender, RoutedEventArgs e)
        {
            if (_vm.Person.Id == 0) return;
            var hc = new HttpClient();
            int id = _vm.Person.Id;
            var xs = new System.Xml.Serialization.XmlSerializer(typeof(Person));
            hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
            var sw = new System.IO.StringWriter();
            // 先頭の <?xml ... をカットする
            var settings = new System.Xml.XmlWriterSettings() { OmitXmlDeclaration = true, Encoding = Encoding.UTF8 };
            var xw = System.Xml.XmlWriter.Create(sw, settings);
            xs.Serialize(xw, _vm.Person);
            var xml = sw.ToString();
            var cont = new StringContent(xml, Encoding.UTF8, "application/xml");
            var res = await hc.PutAsync($"http://localhost:5000/api/People/{id}", cont);
        }

        private async void clickDeleteId(object sender, RoutedEventArgs e)
        {
            if (_vm.Person.Id == 0) return;
            int id = _vm.Person.Id;
            var hc = new HttpClient();
            hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
            var res = await hc.DeleteAsync($"http://localhost:5000/api/People/{id}");
        }
    }
}
