using System.Security.Cryptography.X509Certificates;
using AspCoreRenderSample.Options;

namespace AspCoreRenderSample.Models
{
    public class IndexViewModel
    {
        public DatabaseOptions DbOptions { get; set; }
        public X509Certificate2 DataProtectionCertificate { get; set; }
    }
}