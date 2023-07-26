using Bandit.NBS.Daemon.CommandHandlers;
using Bandit.NBS.Daemon.Extensions;
using Bandit.NBS.Daemon.Helpers;
using Bandit.NBS.MgdbRepository.Repositories;
using Bandit.NBS.MgdbRepository.Utils;
using Bandit.NBS.NpgsqlRepository.Repositories;
using Bandit.NBS.Daemon.Configuration;
using Bandit.NBS.Daemon.Services;
using Bandit.NBS.NpgsqlRepository;
using Microsoft.EntityFrameworkCore;

namespace Bandit.NBS.Daemon
{
    public class Startup
    {
        private IConfiguration _configuration;
        private DaemonConfiguration _parsedConfiguration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
            _parsedConfiguration = configuration.GetSection(DaemonConfiguration.ServiceName).Get<DaemonConfiguration>() ?? new DaemonConfiguration();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services
                .AddSingleton(_parsedConfiguration)
                .AddLogging(b =>
                {
                    b.AddConfiguration(_configuration.GetSection("Logging"));
                    b.AddConsole();
                })
                .AddCommandHandling(b =>
                {
                    b.AddHandler<TransactionsAnalyticsHandler>(TransactionsAnalyticsHandler.Predicate);
                    b.AddHandler<ChallengeAnalyticsHandler>(ChallengeAnalyticsHandler.Predicate);
                })
                .AddSingleton<ICertificateHelper, LocalCertificateHelper>()
                .AddSingleton(provider => MgdbDatabaseFactory.Create(_parsedConfiguration.MgdbDatabase.ConnectionString, _parsedConfiguration.MgdbDatabase.DatabaseName))
                .AddScoped<ICommandParser, CommandParser>()
                .AddScoped<ITransactionRepository, TransactionRepository>()
                .AddScoped<IChallengeRepository, ChallengeRepository>()
                .AddScoped<ITransactionService, TransactionService>()
                .AddScoped<IChallengeService, ChallengeService>()
                .AddHostedService<TCPService>()
                .AddDbContext<NpgsqlDbContext>(options => options.UseNpgsql(_parsedConfiguration.NpgsqlDatabase.ConnectionString));
        }

        public void Configure(IApplicationBuilder app)
        {

        }
    }
}
