using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace SampleSessionMvc.Controllers
{
public class HomeController : Controller
{
    public IActionResult Index()
    {
        this.HttpContext.Session.SetString("now", DateTime.Now.ToString());
        this.HttpContext.Response.Cookies.Append("my-cookie", DateTime.Now.AddDays(100).ToString());
        return View();
    }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }


        public IActionResult Error()
        {
            return View();
        }
    // 内部のフィールド変数
    private string _data = "";
    public IActionResult First()
    {
        // フィールド変数に保存する
        _data = DateTime.Now.ToString();
        ViewData["data"] = _data;
        ViewData["hash"] = this.GetHashCode().ToString("X");
        return View();
    }
    public IActionResult Second()
    {
        // フィールド変数は保存されていない
        ViewData["data"] = _data;
        ViewData["hash"] = this.GetHashCode().ToString("X");
        return View();
    }
    }
}
