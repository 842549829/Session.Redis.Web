using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Session.Redis.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
           var userName = this.HttpContext.Session.GetString("UserName");
            var passWord = this.HttpContext.Session.GetString("PassWord");
            ViewData["UserName"] = userName;
            ViewData["PassWord"] = passWord;
            return View();
        }


        [HttpPost]
        public NoContentResult Add(string userName, string pwd)
        {
            this.HttpContext.Session.SetString("UserName", userName);
            this.HttpContext.Session.SetString("PassWord", pwd);
            return NoContent();
        }
    }
}
