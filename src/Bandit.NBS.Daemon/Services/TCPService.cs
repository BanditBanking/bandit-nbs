using System.Net.Sockets;
using System.Net;
using System.Net.Security;
using Bandit.NBS.Daemon.Configuration;
using Bandit.NBS.Daemon.Helpers;
using Bandit.NBS.Daemon.Clients;
using Bandit.NBS.Daemon.Extensions;

namespace Bandit.NBS.Daemon.Services
{
    public class TCPService : BackgroundService
    {
        private readonly DaemonConfiguration _config;
        private readonly ICertificateHelper _certificateHelper;
        private readonly ILogger<TCPService> _logger;
        private readonly CommandHandlerResolver _handlerResolver;
        private readonly ICommandParser _commandParser;

        public TCPService(DaemonConfiguration config, ICertificateHelper certificateHelper, ILogger<TCPService> logger, CommandHandlerResolver handlerResolver, ICommandParser commandParser)
        {
            _config = config;
            _certificateHelper = certificateHelper;
            _logger = logger;
            _handlerResolver = handlerResolver;
            _commandParser = commandParser;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            await _certificateHelper.LoadCertificateAsync(_config.SSL.ServerCertificate).ConfigureAwait(false);
            _logger.LogInformation($"Starting listening on port {_config.TCP.Port}");
            var endpoint = new TcpListener(IPAddress.Any, _config.TCP.Port);
            endpoint.Start();

            while (!cancellationToken.IsCancellationRequested)
            {
                var client = endpoint.AcceptTcpClient();
                await HandleAsync(client, cancellationToken);
            }
        }

        private async Task HandleAsync(TcpClient client, CancellationToken cancellationToken)
        {
            try
            {
                var remoteIpEndPoint = client.Client.RemoteEndPoint as IPEndPoint;
                _logger.LogInformation($"New connection from {remoteIpEndPoint?.Address}:{remoteIpEndPoint?.Port}");
                using var sslStream = new SslStream(client.GetStream(), false);
                using var sslClient = new SslClient(sslStream);
                sslStream.AuthenticateAsServer(_certificateHelper.GetCertificate(), clientCertificateRequired: false, checkCertificateRevocation: true);
                var commandAsString = await sslClient.ReadStringAsync();
                var command = _commandParser.Parse(commandAsString);
                var commandHandler = _handlerResolver(command);
                await commandHandler.HandleAsync(sslClient, command, cancellationToken);
            } catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                client.Close();
            }
        }
    }
}
