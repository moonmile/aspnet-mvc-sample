using ClientWPF.ViewModels;
using SampleWebApi.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SampleWebApiXamarin
{
public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        this.Appearing += MainPage_Appearing;
    }

    private void MainPage_Appearing(object sender, EventArgs e)
    {
        _vm = new MyViewModel();
        this.BindingContext = _vm;
        // 都道府県をロード
        loadPerfecture();
    }

        private const string SERVER = "172.16.0.11:5000";


    MyViewModel _vm;

    private async void loadPerfecture()
    {
        var hc = new HttpClient();

        var res = await hc.GetAsync($"http://{SERVER}/api/Perfectures");
        var st = await res.Content.ReadAsStreamAsync();
        var js = new Newtonsoft.Json.JsonSerializer();
        var jr = new Newtonsoft.Json.JsonTextReader(new System.IO.StreamReader(st));
        var items = js.Deserialize<List<Perfecture>>(jr);
        _vm.Perfectures = items;

        foreach (var it in items)
        {
            picker.Items.Add(it.Name);
        }
        /*
        picker.SelectedIndexChanged += (_, __) => {
            _vm.Person.PerfectureId = items[picker.SelectedIndex].Id;
        };
        */
    }
    private async void clickGet(object sender, EventArgs e)
    {
        var hc = new HttpClient();
        var res = await hc.GetAsync($"http://{SERVER}/api/People");
        var st = await res.Content.ReadAsStreamAsync();
        var js = new Newtonsoft.Json.JsonSerializer();
        var jr = new Newtonsoft.Json.JsonTextReader(new System.IO.StreamReader(st));
        var items = js.Deserialize<List<Person>>(jr);
        _vm.Persons = items;
    }
    private async void clickGetId(object sender, EventArgs e)
    {
        if (_vm.Person.Id == 0) return;
        int id = _vm.Person.Id;

        var hc = new HttpClient();
        var res = await hc.GetAsync($"http://{SERVER}/api/People/{id}");
        var st = await res.Content.ReadAsStreamAsync();
        var js = new Newtonsoft.Json.JsonSerializer();
        var jr = new Newtonsoft.Json.JsonTextReader(new System.IO.StreamReader(st));
        var item = js.Deserialize<Person>(jr);
        _vm.Person = item;
    }
    private async void clickPost(object sender, EventArgs e)
    {
        var hc = new HttpClient();
        _vm.Person.Id = 0;
        var json = Newtonsoft.Json.JsonConvert.SerializeObject(_vm.Person);
        var cont = new StringContent(json, Encoding.UTF8, "application/json");
        var res = await hc.PostAsync($"http://{SERVER}/api/People", cont);
        var str = await res.Content.ReadAsStringAsync();
        _vm.ID = int.Parse(str);
    }
    private async void clickPutId(object sender, EventArgs e)
    {
        if (_vm.Person.Id == 0) return;
        int id = _vm.Person.Id;
        var json = Newtonsoft.Json.JsonConvert.SerializeObject(_vm.Person);
        var cont = new StringContent(json, Encoding.UTF8, "application/json");
        var hc = new HttpClient();
        var res = await hc.PutAsync($"http://{SERVER}/api/People/{id}", cont);
    }
    private async void clickDeleteId(object sender, EventArgs e)
    {
        if (_vm.Person.Id == 0) return;
        int id = _vm.Person.Id;
        var hc = new HttpClient();
        var res = await hc.DeleteAsync($"http://{SERVER}/api/People/{id}");
    }
}
}
