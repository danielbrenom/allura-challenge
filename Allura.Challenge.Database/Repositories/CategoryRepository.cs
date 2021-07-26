using System.Threading.Tasks;
using Allura.Challenge.Database.Models;
using Allura.Challenge.Database.Repositories.Base;
using Allura.Challenge.Database.Repositories.Interfaces;

namespace Allura.Challenge.Database.Repositories
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