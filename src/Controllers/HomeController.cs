using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using AspCoreFlySample.Models;
using AspCoreFlySample.Options;
using Microsoft.Extensions.Options;

namespace AspCoreFlySample.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOptions<DatabaseOptions> _dbOptions;
        private readonly IOptions<DataProtectionOptions> _dataProtectionOptions;

        public HomeController(IOptions<DatabaseOptions> dbOptions, IOptions<DataProtectionOptions> dataProtectionOptions)
        {
            _dbOptions = dbOptions;
            _dataProtectionOptions = dataProtectionOptions;
        }

        public IActionResult Index()
        {
            // var cert = X509Certificate2.CreateFromPem(_dataProtectionOptions.Value.Certificate, _dataProtectionOptions.Value.PrivateKey);

            return View(new IndexViewModel
            {
                DbOptions = _dbOptions.Value,
                CertString = _dataProtectionOptions.Value.Certificate,
                PrivateKeyString = _dataProtectionOptions.Value.PrivateKey
                // DataProtectionCertificate = cert,
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
