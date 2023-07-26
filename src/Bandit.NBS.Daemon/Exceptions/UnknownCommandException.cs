namespace Bandit.NBS.Daemon.Exceptions
{
    [Serializable]
    public class UnknownCommandException : Exception
    {
        public UnknownCommandException() { }

        public UnknownCommandException(string message) : base(message) { }
    }
}
