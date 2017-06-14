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

namespace ClientJson
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
            var res = await hc.GetAsync("http://localhost:5000/api/People");
            var str = await res.Content.ReadAsStringAsync();
            textResult.Text = str;
        }

        private int _id = 1;


        private async void clickGetId(object sender, RoutedEventArgs e)
        {
            var hc = new HttpClient();
            var res = await hc.GetAsync($"http://localhost:5000/api/People/{_id}");
            var str = await res.Content.ReadAsStringAsync();
            textResult.Text = str;
        }

        private async void clickPost(object sender, RoutedEventArgs e)
        {
            var hc = new HttpClient();
            var data = new Dictionary<string, string> {
                { "Name", "new person" },
                { "EmployeeNo", "ABC-9999" },
                { "PrefectureId", "1"  },
                { "Age", "99" }
            };
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            var cont = new StringContent(json, Encoding.UTF8, "application/json");
            var res = await hc.PostAsync("http://localhost:5000/api/People", cont);
            var str = await res.Content.ReadAsStringAsync();
            textResult.Text = str;
            _id = int.Parse(str);
        }

        private async void clickPutId(object sender, RoutedEventArgs e)
        {
            var hc = new HttpClient();
            var data = new Dictionary<string, string> {
                { "Id", _id.ToString() },
                { "Name", "update person" },
                { "EmployeeNo", "ABC-9999" },
                { "PrefectureId", "1"  },
                { "Age", "99" }
            };
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            var cont = new StringContent(json, Encoding.UTF8, "application/json");
            var res = await hc.PutAsync($"http://localhost:5000/api/People/{_id}", cont);
            var str = await res.Content.ReadAsStringAsync();
            textResult.Text = str;
        }
    }
}
