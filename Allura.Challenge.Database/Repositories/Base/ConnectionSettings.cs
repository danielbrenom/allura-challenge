using Allura.Challenge.Database.Repositories.Interfaces;

namespace Allura.Challenge.Database.Repositories.Base
{
    public class ConnectionSettings : IConnectionSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}