using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace OptionSample.Controllers
{
    public class HomeController : Controller
    {
        public Class1 _class1 { get; set; }
        public HomeController(IOptions<Class1> class1)
        {
            _class1 = class1.Value;
        }
        public IActionResult Index()
        {
            return View(_class1);
        }
    }
}