using System.Security.Cryptography.X509Certificates;

namespace Bandit.NBS.Daemon.Helpers
{
    public interface ICertificateHelper
    {
        Task LoadCertificateAsync(string fileName);
        X509Certificate2 GetCertificate();
    }
}
