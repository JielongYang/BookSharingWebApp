using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using _netmvc.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
namespace _netmvc.Controllers
{
    public class HomeController : Controller
    {
    
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
       
    }
}
