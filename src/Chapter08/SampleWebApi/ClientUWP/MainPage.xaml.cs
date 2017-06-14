using ClientUWP.ViewModels;
using SampleWebApi.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 を参照してください

namespace ClientUWP
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
public sealed partial class MainPage : Page
{
    public MainPage()
    {
        this.InitializeComponent();
        this.Loaded += MainPage_Loaded;
    }

    private async void MainPage_Loaded(object sender, RoutedEventArgs e)
    {
        _vm = new MyViewModel();
        this.DataContext = _vm;
            // 都道府県データを読み込む
            var hc = new HttpClient();
            var res = await hc.GetAsync("http://localhost:5000/api/Prefectures");
            var st = await res.Content.ReadAsStreamAsync();
            var js = new Newtonsoft.Json.JsonSerializer();
            var jr = new Newtonsoft.Json.JsonTextReader(new System.IO.StreamReader(st));
            var items = js.Deserialize<List<Prefecture>>(jr);
            _vm.Prefectures = items;
        }
        MyViewModel _vm;

        private async void clickGet(object sender, RoutedEventArgs e)
        {
            var hc = new HttpClient();
            var res = await hc.GetAsync("http://localhost:5000/api/People");
            var st = await res.Content.ReadAsStreamAsync();
            var js = new Newtonsoft.Json.JsonSerializer();
            var jr = new Newtonsoft.Json.JsonTextReader(new System.IO.StreamReader(st));
            var items = js.Deserialize<List<Person>>(jr);
            _vm.Persons = items;
        }

        private async void clickGetId(object sender, RoutedEventArgs e)
        {
            if (_vm.Person.Id == 0) return;
            int id = _vm.Person.Id;

            var hc = new HttpClient();
            var res = await hc.GetAsync($"http://localhost:5000/api/People/{id}");
            var st = await res.Content.ReadAsStreamAsync();
            var js = new Newtonsoft.Json.JsonSerializer();
            var jr = new Newtonsoft.Json.JsonTextReader(new System.IO.StreamReader(st));
            var item = js.Deserialize<Person>(jr);
            _vm.Person = item;

        }

        private async void clickPost(object sender, RoutedEventArgs e)
        {
            var hc = new HttpClient();
            _vm.Person.Id = 0;
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(_vm.Person);
            var cont = new StringContent(json, Encoding.UTF8, "application/json");
            var res = await hc.PostAsync("http://localhost:5000/api/People", cont);
            var str = await res.Content.ReadAsStringAsync();
            // textId.Text = str;
            _vm.ID = int.Parse(str);
        }

        private async void clickPutId(object sender, RoutedEventArgs e)
        {
            if (_vm.Person.Id == 0) return;
            int id = _vm.Person.Id;
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(_vm.Person);
            var cont = new StringContent(json, Encoding.UTF8, "application/json");
            var hc = new HttpClient();
            var res = await hc.PutAsync($"http://localhost:5000/api/People/{id}", cont);
        }

        private async void clickDeleteId(object sender, RoutedEventArgs e)
        {
            if (_vm.Person.Id == 0) return;
            int id = _vm.Person.Id;
            var hc = new HttpClient();
            var res = await hc.DeleteAsync($"http://localhost:5000/api/People/{id}");
        }
    }
}
