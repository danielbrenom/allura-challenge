namespace Allura.Challenge.Database.Repositories.Interfaces
{
    public interface IBasePaginator
    {
        int ItemsPerPage { get; set; }
        int Page { get; set; }
    }
}