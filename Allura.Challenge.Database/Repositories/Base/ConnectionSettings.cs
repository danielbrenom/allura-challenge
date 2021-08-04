using Alura.Challenge.Database.Repositories.Interfaces;

namespace Alura.Challenge.Database.Repositories.Base
{
    public class ConnectionSettings : IConnectionSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}