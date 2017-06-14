using System;
using System.Collections.Generic;
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
using SampleWebApiXml.Models;

namespace ClientXml2
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
            dataGrid.AutoGenerateColumns = false;
            dataGrid.IsReadOnly = true;
            // 都道府県データを読み込む
            var hc = new HttpClient();
            hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
            var res = await hc.GetAsync("http://localhost:5000/api/Prefectures");
            var st = await res.Content.ReadAsStreamAsync();
            var xs = new System.Xml.Serialization.XmlSerializer(typeof(Prefectures));
            var obj = xs.Deserialize(st) as Prefectures;
            comboPrefecture.ItemsSource = obj.Items;
        }

        private async void clickGet(object sender, RoutedEventArgs e)
        {
            var hc = new HttpClient();
            hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
            var res = await hc.GetAsync("http://localhost:5000/api/People");
            var st = await res.Content.ReadAsStreamAsync();
            /*
            var xs = new System.Xml.Serialization.XmlSerializer(typeof(People));
            var obj = xs.Deserialize(st) as People;
            dataGrid.ItemsSource = obj.Items;
            */
            var xs = new System.Xml.Serialization.XmlSerializer(typeof(object));
            var obj = xs.Deserialize(st) as dynamic;
            dataGrid.ItemsSource = obj.Items;
        }

        private async void clickGetId(object sender, RoutedEventArgs e)
        {
            if (textId.Text == "") return;

            int id = int.Parse(textId.Text);
            var hc = new HttpClient();
            hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
            var res = await hc.GetAsync($"http://localhost:5000/api/People/{id}");
            var st = await res.Content.ReadAsStreamAsync();
            var xs = new System.Xml.Serialization.XmlSerializer(typeof(Person));
            var item = xs.Deserialize(st) as Person;
            textName.Text = item.Name;
            textAge.Text = item.Age.ToString();
            comboPrefecture.SelectedValue = item.PrefectureId;

        }

        private async void clickPost(object sender, RoutedEventArgs e)
        {
            var person = new Person
            {
                Name = textName.Text,
                Age = int.Parse( textAge.Text ) ,
                PrefectureId = (int)comboPrefecture.SelectedValue
            };
            var xs = new System.Xml.Serialization.XmlSerializer(typeof(Person));
            var hc = new HttpClient();
            hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
            var sw = new System.IO.StringWriter();
            // 先頭の <?xml ... をカットする
            var settings = new System.Xml.XmlWriterSettings() { OmitXmlDeclaration = true, Encoding = Encoding.UTF8 };
            var xw = System.Xml.XmlWriter.Create(sw, settings);
            xs.Serialize(xw, person);
            var xml = sw.ToString();
            var cont = new StringContent(xml, Encoding.UTF8, "application/xml");
            var res = await hc.PostAsync("http://localhost:5000/api/people", cont);
            var st = await res.Content.ReadAsStreamAsync();
            var xs2 = new System.Xml.Serialization.XmlSerializer(typeof(int));
            int id = (int)xs2.Deserialize(st);
            textId.Text = id.ToString();
        }

        private async void clickPutId(object sender, RoutedEventArgs e)
        {
            if (textId.Text == "") return;
            var person = new Person
            {
                Id = int.Parse( textId.Text),
                Name = textName.Text,
                Age = int.Parse(textAge.Text),
                PrefectureId = (int)comboPrefecture.SelectedValue
            };
            var xs = new System.Xml.Serialization.XmlSerializer(typeof(Person));
            var hc = new HttpClient();
            hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
            var sw = new System.IO.StringWriter();
            // 先頭の <?xml ... をカットする
            var settings = new System.Xml.XmlWriterSettings() { OmitXmlDeclaration = true, Encoding = Encoding.UTF8 };
            var xw = System.Xml.XmlWriter.Create(sw, settings);
            xs.Serialize(xw, person);
            var xml = sw.ToString();
            var cont = new StringContent(xml, Encoding.UTF8, "application/xml");
            var res = await hc.PutAsync($"http://localhost:5000/api/people/{person.Id}", cont);
        }

        private async void clickDeleteId(object sender, RoutedEventArgs e)
        {
            if (textId.Text == "") return;

            int id = int.Parse(textId.Text);
            var hc = new HttpClient();
            hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
            var res = await hc.DeleteAsync($"http://localhost:5000/api/People/{id}");
        }
    }
}
