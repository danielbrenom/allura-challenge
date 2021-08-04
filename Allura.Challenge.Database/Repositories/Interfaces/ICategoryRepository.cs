using System.Threading.Tasks;
using Alura.Challenge.Database.Models;

namespace Alura.Challenge.Database.Repositories.Interfaces
{
    public interface ICategoryRepository<T> : IBaseRepository<T>
    {
        Task<Category> GetDefaultCategory();
    }
}