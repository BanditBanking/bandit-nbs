using Bandit.NBS.MgdbRepository.Utils;

namespace Bandit.NBS.Daemon.Extensions
{
    public static class ServiceCollectionExtension
    {

        public static IServiceCollection AddCommandHandling(this IServiceCollection services, Action<ICommandHandlingBuilder> configure)
        {
            var builder = new CommandHandlingBuilder(services);
            configure(builder);
            builder.RegisterHandlers();
            return services;
        }
    }
}
