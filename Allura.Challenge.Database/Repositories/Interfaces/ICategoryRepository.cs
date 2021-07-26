using System.Threading.Tasks;

namespace Allura.Challenge.Database.Repositories.Interfaces
{
    public interface ICategoryRepository<T> : IBaseRepository<T>
    {
        Task<Models.Category> GetDefaultCategory();
    }
}