using System.Security.Cryptography.X509Certificates;
using AspCoreK8sSample.Options;

namespace AspCoreK8sSample.Models
{
    public class IndexViewModel
    {
        public DatabaseOptions DbOptions { get; set; }
        public Auth0Options Auth0Options { get; set; }
        public X509Certificate2 DataProtectionCertificate { get; set; }
    }
}