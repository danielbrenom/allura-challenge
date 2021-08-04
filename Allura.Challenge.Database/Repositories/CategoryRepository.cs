using System.Threading.Tasks;
using Alura.Challenge.Database.Models;
using Alura.Challenge.Database.Repositories.Base;
using Alura.Challenge.Database.Repositories.Interfaces;

namespace Alura.Challenge.Database.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository<Category>
    {
        protected override string CollectionName => "Categories";

        public CategoryRepository(IConnectionSettings connectionSettings) : base(connectionSettings)
        {
        }

        public async Task<Category> GetDefaultCategory()
        {
            var category = await GetAsync("1");
            if (!(category is null)) return category;
            var defaultCategory = Category.CreateDefault();
            var result = await AddAsync(defaultCategory);
            if (result)
                return defaultCategory;
            throw new System.Exception("Unable to create/get default category");
        }
    }
}