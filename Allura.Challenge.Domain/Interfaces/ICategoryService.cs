﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Allura.Challenge.Domain.Models.Data;
using Allura.Challenge.Domain.Models.Requests;

namespace Allura.Challenge.Domain.Interfaces
{
    public interface ICategoryService
    {
        Task<Category> GetCategory(string id);
        Task<List<Category>> GetCategories();
        Task<Category> CreateCategory(CategoryRequest categoryRequest);
        Task<Category> UpdateCategory(CategoryRequest categoryRequest, string id);
        Task<bool> DeleteCategory(string id);
    }
}