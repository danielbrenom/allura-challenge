namespace Alura.Challenge.Database.Repositories.Interfaces
{
    public interface IConnectionSettings
    {
        string ConnectionString { get; }
        string DatabaseName { get; }
    }
}