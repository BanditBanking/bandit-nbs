namespace Bandit.NBS.Daemon.Configuration
{
    public class DaemonConfiguration
    {
        public const string ServiceName = "NBS";
        public DatabaseConfiguration MgdbDatabase { get; set; }
        public DatabaseConfiguration NpgsqlDatabase { get; set; }
        public TCPConfiguration TCP { get; set; }
        public SSLConfiguration SSL { get; set; }

    }
}
