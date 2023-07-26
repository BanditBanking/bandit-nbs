using Bandit.NBS.MgdbRepository.Utils;
using Bandit.NBS.NpgsqlRepository;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace Bandit.NBS.Daemon
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            EnsureEfMigration(host);

            await MgdbIndexer.CreateIndexesAsync(host.Services.GetService<IMongoDatabase>());

            await host.RunAsync().ConfigureAwait(false);
        }

        // Due to an EF bug, need to use an host builder: https://stackoverflow.com/questions/55970148/apply-entity-framework-migrations-when-using-asp-net-core-in-a-docker-image
        public static IWebHostBuilder CreateHostBuilder(string[] args)
        {
            var builder = WebHost.CreateDefaultBuilder().ConfigureAppConfiguration(config =>
            {
                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                config.AddEnvironmentVariables();
            }).UseStartup<Startup>();

            return builder;
        }

        private static void EnsureEfMigration(IWebHost host)
        {
            using var scope = host.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<NpgsqlDbContext>();
            if (context.Database.GetPendingMigrations().Any())
                context.Database.Migrate();
        }
    }
}
