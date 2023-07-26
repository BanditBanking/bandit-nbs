using Bandit.NBS.Daemon.Configuration;
using System.Security.Cryptography.X509Certificates;

namespace Bandit.NBS.Daemon.Helpers
{
    public class LocalCertificateHelper : ICertificateHelper
    {
        private string _storePath;
        private ILogger<LocalCertificateHelper> _logger;
        private X509Certificate2 _certificate;

        public LocalCertificateHelper(ILogger<LocalCertificateHelper> logger, DaemonConfiguration config)
        {
            _storePath = config?.SSL?.StorePath ?? "/certs";
            _logger = logger;
        }

        public X509Certificate2 GetCertificate() => _certificate;

        public async Task LoadCertificateAsync(string fileName)
        {
            try
            {
                byte[] certBytes = await File.ReadAllBytesAsync(Path.Combine(_storePath, fileName));
                _certificate =  new X509Certificate2(certBytes);
                _logger.LogInformation($"Loaded server certificate : {_certificate.Subject}");
            }
            catch (FileNotFoundException)
            {
                _logger.LogCritical($"Certificate not found : {Path.Combine(_storePath, fileName)}");
                throw;
            }
        }
    }
}
