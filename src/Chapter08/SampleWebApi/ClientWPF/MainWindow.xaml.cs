using ClientUWP.ViewModels;
using SampleWebApi.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ClientUWP
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
        var res = await hc.GetAsync("http://localhost:5000/api/Perfectures");
        var st = await res.Content.ReadAsStreamAsync();
        var js = new Newtonsoft.Json.JsonSerializer();
        var jr = new Newtonsoft.Json.JsonTextReader(new System.IO.StreamReader(st));
        var items = js.Deserialize<List<Perfecture>>(jr);
        _vm.Perfectures = items;

    }
    MyViewModel _vm;
    /// <summary>
    /// Getボタンのクリックイベント
    /// </summary>
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
    /// <summary>
    /// GetIDボタンのクリックイベント
    /// </summary>
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
    /// <summary>
    /// Postボタンのクリックイベント
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void clickPost(object sender, RoutedEventArgs e)
    {
        var hc = new HttpClient();
        _vm.Person.Id = 0;
        var json = Newtonsoft.Json.JsonConvert.SerializeObject(_vm.Person);
        var cont = new StringContent(json, Encoding.UTF8, "application/json");
        var res = await hc.PostAsync("http://localhost:5000/api/People", cont);
        var str = await res.Content.ReadAsStringAsync();
        _vm.ID = int.Parse(str);
    }
    /// <summary>
    /// PutIDボタンのクリックイベント
    /// </summary>
    private async void clickPutId(object sender, RoutedEventArgs e)
    {
        if (_vm.Person.Id == 0) return;
        int id = _vm.Person.Id;
        var json = Newtonsoft.Json.JsonConvert.SerializeObject(_vm.Person);
        var cont = new StringContent(json, Encoding.UTF8, "application/json");
        var hc = new HttpClient();
        var res = await hc.PutAsync($"http://localhost:5000/api/People/{id}", cont);
    }
    /// <summary>
    /// DeleteIDボタンのクリックイベント
    /// </summary>
    private async void clickDeleteId(object sender, RoutedEventArgs e)
    {
        if (_vm.Person.Id == 0) return;
        int id = _vm.Person.Id;
        var hc = new HttpClient();
        var res = await hc.DeleteAsync($"http://localhost:5000/api/People/{id}");
    }
}
}
