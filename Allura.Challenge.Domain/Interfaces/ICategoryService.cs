using System.Collections.Generic;
using System.Threading.Tasks;
using Alura.Challenge.Domain.Models.Data;
using Alura.Challenge.Domain.Models.Requests;

namespace Alura.Challenge.Domain.Interfaces
{
    public interface ICategoryService
    {
        Task<Category> GetCategory(string id);
        Task<List<Category>> GetCategories(string page);
        Task<Category> CreateCategory(CategoryRequest categoryRequest);
        Task<Category> UpdateCategory(CategoryRequest categoryRequest, string id);
        Task<bool> DeleteCategory(string id);
    }
}