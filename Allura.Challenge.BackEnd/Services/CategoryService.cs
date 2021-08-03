using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Allura.Challenge.BackEnd.Extensions;
using Allura.Challenge.Database.Models;
using Allura.Challenge.Database.Repositories.Base;
using Allura.Challenge.Database.Repositories.Interfaces;
using Allura.Challenge.Domain.Exceptions;
using Allura.Challenge.Domain.Interfaces;
using Allura.Challenge.Domain.Models.Requests;
using Allura.Challenge.Domain.Validators.Interfaces;

namespace Allura.Challenge.BackEnd.Services
{
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository<Category> Repository { get; }
        private IValidator<Domain.Models.Data.Category> Validator { get; }

        public CategoryService(ICategoryRepository<Category> repository, IValidator<Domain.Models.Data.Category> validator)
        {
            Repository = repository;
            Validator = validator;
        }

        public async Task<Domain.Models.Data.Category> GetCategory(string id)
        {
            var result = await Repository.GetAsync(id);
            if (result is null)
                throw new GenericException("Category not found", 404);
            return result.GetAs<Domain.Models.Data.Category>();
        }

        public async Task<List<Domain.Models.Data.Category>> GetCategories(string page)
        {
            var paginator = new BasePaginator(5) {Page = string.IsNullOrEmpty(page) ? 1 : int.Parse(page)};
            var result = await Repository.GetAllAsync(paginator);
            return result.ToList().GetAs<List<Domain.Models.Data.Category>>();
        }

        public async Task<Domain.Models.Data.Category> CreateCategory(CategoryRequest categoryRequest)
        {
            var category = categoryRequest.GetAs<Domain.Models.Data.Category>();
            var validationResult = await Validator.CheckAsync(category, "Create");
            if (!validationResult.IsValid)
                throw new InvalidDataException("Category is invalid", validationResult.Errors);
            category.Id = System.Guid.NewGuid().ToString("N");
            var result = await Repository.AddAsync(category.GetAs<Category>());
            if (!result)
                throw new System.Exception("An error occurred creating the category");
            return category;
        }

        public async Task<Domain.Models.Data.Category> UpdateCategory(CategoryRequest categoryRequest, string id)
        {
            var category = categoryRequest.GetAs<Domain.Models.Data.Category>();
            var validationResult = await Validator.CheckAsync(category, "Update");
            if (!validationResult.IsValid)
                throw new InvalidDataException("Category is invalid", validationResult.Errors);
            category.Id = id;
            var result = await Repository.UpdateAsync(category.GetAs<Category>());
            if (!result)
                throw new System.Exception("An error occurred updating the category");
            return category;
        }

        public async Task<bool> DeleteCategory(string id)
        {
            var category = await Repository.GetAsync(id);
            if (category is null)
                throw new GenericException("Category not found", 404);
            var result = await Repository.DeleteAsync(category);
            if (!result)
                throw new GenericException("Could not delete category", 500);
            return true;
        }
    }
}