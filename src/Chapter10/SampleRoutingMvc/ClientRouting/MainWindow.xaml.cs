using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace ClientRouting
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
            var res = await hc.GetAsync("http://localhost:5000/api/people");
            var str = await res.Content.ReadAsStringAsync();
            text1.Text = str;
        }

        private async void clickGetId(object sender, RoutedEventArgs e)
        {
            int id = 2;
            var hc = new HttpClient();
            var res = await hc.GetAsync($"http://localhost:5000/api/people/{id}");
            var str = await res.Content.ReadAsStringAsync();
            text1.Text = str;
        }

        private async void clickPost(object sender, RoutedEventArgs e)
        {
            var person = new Person() { Name = "new person", Age = 99 };
            var hc = new HttpClient();
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(person);
            var cont = new StringContent(json, Encoding.UTF8, "application/json");
            var res = await hc.PostAsync("http://localhost:5000/api/people", cont);
            var str = await res.Content.ReadAsStringAsync();
            text1.Text = str;
        }

        private async void clickPutId(object sender, RoutedEventArgs e)
        {
            int id = 2;
            var person = new Person() { Id = id, Name = "update person", Age = 80 };
            var hc = new HttpClient();
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(person);
            var cont = new StringContent(json, Encoding.UTF8, "application/json");
            var res = await hc.PutAsync($"http://localhost:5000/api/people/{id}", cont);
            var str = await res.Content.ReadAsStringAsync();
            text1.Text = str;
        }

        private async void clickCreate(object sender, RoutedEventArgs e)
        {
            var person = new Person() { Name = "create person", Age = 99 };
            var hc = new HttpClient();
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(person);
            var cont = new StringContent(json, Encoding.UTF8, "application/json");
            var res = await hc.PostAsync("http://localhost:5000/api/people/Create", cont);
            var str = await res.Content.ReadAsStringAsync();
            text1.Text = str;
        }

        private async void clickUpdate(object sender, RoutedEventArgs e)
        {
            var person = new Person() { Id = 2, Name = "UPDATE person", Age = 88 };
            var hc = new HttpClient();
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(person);
            var cont = new StringContent(json, Encoding.UTF8, "application/json");
            var res = await hc.PostAsync($"http://localhost:5000/api/people/Update/{person.Id}", cont);
            var str = await res.Content.ReadAsStringAsync();
            text1.Text = str;
        }

private async void clickSearch(object sender, RoutedEventArgs e)
{
    string value = text2.Text;
    var hc = new HttpClient();
    var res = await hc.GetAsync($"http://localhost:5000/api/people/search/name/{value}");
    var str = await res.Content.ReadAsStringAsync();
    text1.Text = str;
}

        private async void clickSearchAge(object sender, RoutedEventArgs e)
        {
            int value = int.Parse( text3.Text) ;
            var hc = new HttpClient();
            var res = await hc.GetAsync($"http://localhost:5000/api/people/search/age/{value}");
            var str = await res.Content.ReadAsStringAsync();
            text1.Text = str;
        }
    }
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
