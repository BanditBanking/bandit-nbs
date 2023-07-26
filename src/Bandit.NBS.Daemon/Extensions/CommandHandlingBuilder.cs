using Bandit.NBS.Daemon.Exceptions;
using Bandit.NBS.Daemon.CommandHandlers;
using Bandit.NBS.Daemon.Commands;

namespace Bandit.NBS.Daemon.Extensions
{
    public delegate ICommandHandler CommandHandlerResolver(ICommand command);

    public class CommandHandlingBuilder : ICommandHandlingBuilder
    {
        private readonly IServiceCollection _services;
        private readonly List<(Type T, Predicate<ICommand>)> _handlerDescriptors;

        public CommandHandlingBuilder(IServiceCollection services)
        {
            _services = services;
            _handlerDescriptors = new List<(Type T, Predicate<ICommand>)>();
        }

        void ICommandHandlingBuilder.AddHandler<T>(Predicate<ICommand> predicate)
        {
            _handlerDescriptors.Add((typeof(T), predicate));
            _services.AddScoped(typeof(T));
        }

        internal void RegisterHandlers()
        {
            _services.AddTransient<CommandHandlerResolver>(provider => command =>
            {
                foreach (var (handlerType, predicate) in _handlerDescriptors)
                {
                    if (predicate(command))
                        return provider.GetService(handlerType) as ICommandHandler ?? throw new UnknownCommandException($"Handler not registered for command {command.Type}");
                }
                throw new UnknownCommandException($"Unknown command {command.Type}");
            });
        }
    }
}
