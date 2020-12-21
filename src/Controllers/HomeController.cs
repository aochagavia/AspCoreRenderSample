using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
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
        private readonly IOptions<DataProtectionOptions> _dataProtectionOptions;

        public HomeController(IOptions<DatabaseOptions> dbOptions, IOptions<Auth0Options> authOptions, IOptions<DataProtectionOptions> dataProtectionOptions)
        {
            _dbOptions = dbOptions;
            _authOptions = authOptions;
            _dataProtectionOptions = dataProtectionOptions;
        }

        public IActionResult Index()
        {
            var cert = X509Certificate2.CreateFromPem(_dataProtectionOptions.Value.Certificate, _dataProtectionOptions.Value.PrivateKey);

            return View(new IndexViewModel
            {
                DbOptions = _dbOptions.Value,
                Auth0Options = _authOptions.Value,
                DataProtectionCertificate = cert,
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
