using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspCoreK8sSample.Models;
using AspCoreK8sSample.Options;
using Microsoft.Extensions.Options;

namespace AspCoreK8sSample.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOptions<DatabaseOptions> _dbOptions;
        private readonly IOptions<Auth0Options> _authOptions;

        public HomeController(IOptions<DatabaseOptions> dbOptions, IOptions<Auth0Options> authOptions)
        {
            _dbOptions = dbOptions;
            _authOptions = authOptions;
        }

        public IActionResult Index()
        {
            return View(new IndexViewModel
            {
                DbOptions = _dbOptions.Value,
                Auth0Options = _authOptions.Value
            });
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
