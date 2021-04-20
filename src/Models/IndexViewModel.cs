using System.Security.Cryptography.X509Certificates;
using AspCoreFlySample.Options;

namespace AspCoreFlySample.Models
{
    public class IndexViewModel
    {
        public DatabaseOptions DbOptions { get; set; }
        public string CertString { get; set; }
        public string PrivateKeyString { get; set; }
        // public X509Certificate2 DataProtectionCertificate { get; set; }
    }
}