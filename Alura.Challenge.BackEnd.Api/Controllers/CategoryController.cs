using System.Collections.Generic;
using System.Threading.Tasks;
using Alura.Challenge.BackEnd.Functions.Extensions;
using Alura.Challenge.Domain.Interfaces;
using Alura.Challenge.Domain.Models.Requests;
using Alura.Challenge.Domain.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Alura.Challenge.BackEnd.Api.Controllers
{
    [ApiController]
    public class CategoryController : Controller
    {
        
        private ICategoryService CategoryService { get; }
        private IMovieService MovieService { get; }

        public CategoryController(ICategoryService categoryService, IMovieService movieService)
        {
            CategoryService = categoryService;
            MovieService = movieService;
        }
        
        /// <summary>
        /// Gets a category from the database
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("/categorias/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var category = await CategoryService.GetCategory(id);
            return new JsonResult(category.GetAs<CategoryResponse>());
        }
        
        /// <summary>
        /// Gets all categories from the database
        /// </summary>
        /// <param name="page"></param>
        [HttpGet("/categorias")]
        public async Task<IActionResult> GetAll([FromQuery] string page)
        {
            var category = await CategoryService.GetCategories(page);
            return new JsonResult(category.GetAs<CategoryResponse>());
        }
        
        /// <summary>
        /// Gets all movies by category from the database
        /// </summary>
        /// <param name="id">Category Id</param>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet("/categorias/{id}/videos")]
        public async Task<IActionResult> GetAllByCategory(string id, [FromQuery] string page)
        {
            var moviesByCategory = await MovieService.GetMoviesByCategory(id, page);
            return new JsonResult(new MovieByCategoryResponse {Movies = moviesByCategory.GetAs<List<MovieResponse>>()});
        }

        /// <summary>
        /// Creates a category in the database
        /// </summary>
        /// <param name="data">CAtegory data</param>
        /// <returns></returns>
        [HttpPost("/categorias")]
        public async Task<IActionResult> Create([FromBody] CategoryRequest data)
        {
            var category = await CategoryService.CreateCategory(data);
            return new CreatedResult("/categorias", category.GetAs<CategoryResponse>());
        }

        /// <summary>
        /// Updates a category in the database
        /// </summary>
        /// <param name="id">Category Id</param>
        /// <param name="data">Category data to update</param>
        /// <returns></returns>
        [HttpPatch("/categorias/{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] CategoryRequest data)
        {
            var updatedCategory = await CategoryService.UpdateCategory(data, id);
            return new JsonResult(updatedCategory.GetAs<CategoryResponse>()) {StatusCode = 200};
        }

        /// <summary>
        /// Deletes a category from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("/categorias/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await CategoryService.DeleteCategory(id);
            return new OkResult();
        }
    }
}