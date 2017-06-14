using SampleWebApiXml.Models;
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

namespace ClientXml
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void clickGet(object sender, RoutedEventArgs e)
        {
            var hc = new HttpClient();
            hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
            var res = await hc.GetAsync("http://localhost:5000/api/People");
            var str = await res.Content.ReadAsStringAsync();
            textResult.Text = str;
        }

        private int _id = 3;


        private async void clickGetId(object sender, RoutedEventArgs e)
        {
            var hc = new HttpClient();
            hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
            var res = await hc.GetAsync($"http://localhost:5000/api/People/{_id}");
            var str = await res.Content.ReadAsStringAsync();
            textResult.Text = str;
        }



        private async void clickPost(object sender, RoutedEventArgs e)
        {
            var person = new Person
            {
                Name = "new person",
                Age = 99,
                PrefectureId = 1
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
            var str = await res.Content.ReadAsStringAsync();
            textResult.Text = str;
            var xs2 = new System.Xml.Serialization.XmlSerializer(typeof(int));
            var _id = xs2.Deserialize(new System.IO.StringReader(str));
        }

        private async void clickPutId(object sender, RoutedEventArgs e)
        {
            var person = new Person
            {
                Id = _id,
                Name = "update person",
                Age = 100,
                PrefectureId = 1
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
            var res = await hc.PutAsync($"http://localhost:5000/api/people/{_id}", cont);
            var str = await res.Content.ReadAsStringAsync();
            textResult.Text = str;
        }
    }
}
