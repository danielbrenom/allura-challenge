using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Allura.Challenge.BackEnd.Extensions;
using Allura.Challenge.Domain.Interfaces;
using Allura.Challenge.Domain.Models.Requests;
using Allura.Challenge.Domain.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace Allura.Challenge.BackEnd.Routes
{
    public class Categories
    {
        private ICategoryService CategoryService { get; }

        public Categories(ICategoryService categoryService)
        {
            CategoryService = categoryService;
        }
        
        [OpenApiOperation("Get Category", "Get", Summary = "Get Category", Description = "Gets a Category from the database", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiParameter("id", In = ParameterLocation.Path, Required = true, Type = typeof(string))]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(CategoryResponse))]
        [FunctionName(nameof(Categories) + nameof(Get))]
        public async Task<IActionResult> Get([HttpTrigger(AuthorizationLevel.Function, "get", Route = "categorias/{id}")]
            HttpRequest req, string id, ILogger log)
        {
            try
            {
                var category = await CategoryService.GetCategory(id);
                return new JsonResult(category.GetAs<CategoryResponse>());
            }
            catch (Domain.Exceptions.GenericException e)
            {
                return new JsonResult(e.Message) {StatusCode = e.StatusCode};
            }
            catch (System.Exception e)
            {
                return new JsonResult(e.Message) {StatusCode = 500};
            }
        }
        
        [OpenApiOperation("Get Categories", "Get", Summary = "Get Categories", Description = "Gets all Categories from the database", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(List<CategoryResponse>))]
        [FunctionName(nameof(Categories) + nameof(GetAll))]
        public async Task<IActionResult> GetAll([HttpTrigger(AuthorizationLevel.Function, "get", Route = "categorias")]
            HttpRequest req, ILogger log)
        {
            try
            {
                var category = await CategoryService.GetCategories();
                return new JsonResult(category.GetAs<List<CategoryResponse>>());
            }
            catch (Domain.Exceptions.GenericException e)
            {
                return new JsonResult(e.Message) {StatusCode = e.StatusCode};
            }
            catch (System.Exception e)
            {
                return new JsonResult(e.Message) {StatusCode = 500};
            }
        }

        [OpenApiOperation("Add Category", "Add", Summary = "Add Category", Description = "Adds a new Category to the database", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiRequestBody("application/json", typeof(CategoryRequest), Required = true)]
        [OpenApiResponseWithBody(HttpStatusCode.Created, "application/json", typeof(CategoryResponse))]
        [FunctionName(nameof(Categories) + nameof(Add))]
        public async Task<IActionResult> Add([HttpTrigger(AuthorizationLevel.Function, "post", Route = "categorias")]
            HttpRequest req, ILogger log)
        {
            try
            {
                var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var data = JsonConvert.DeserializeObject<CategoryRequest>(requestBody);
                var category = await CategoryService.CreateCategory(data);
                return new CreatedResult("/categorias", category.GetAs<CategoryResponse>());
            }
            catch (Domain.Exceptions.InvalidDataException e)
            {
                return new JsonResult(e.ValidationErrors) {StatusCode = 422};
            }
            catch (System.Exception e)
            {
                return new JsonResult(e.Message) {StatusCode = 500};
            }
        }
        
        [OpenApiOperation("Update Category", "Update", Summary = "Update Category", Description = "Updates a Category from the database", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiParameter("id", In = ParameterLocation.Path, Required = true, Type = typeof(string))]
        [OpenApiRequestBody("application/json", typeof(CategoryRequest), Required = true)]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(CategoryResponse))]
        [FunctionName(nameof(Categories) + nameof(Update))]
        public async Task<IActionResult> Update([HttpTrigger(AuthorizationLevel.Function, "put", "patch", Route = "categorias/{id}")]
            HttpRequest req, string id, ILogger log)
        {
            try
            {
                var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var data = JsonConvert.DeserializeObject<CategoryRequest>(requestBody);
                var updatedCategory = await CategoryService.UpdateCategory(data, id);
                return new JsonResult(updatedCategory.GetAs<CategoryResponse>()) {StatusCode = 200};
            }
            catch (Domain.Exceptions.InvalidDataException e)
            {
                return new JsonResult(e.ValidationErrors) {StatusCode = 422};
            }
            catch (System.Exception e)
            {
                return new JsonResult(e.Message) {StatusCode = 500};
            }
        }
        
        [OpenApiOperation("Delete Category", "Delete", Summary = "Deletes Category", Description = "Deletes a Category from the database", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiParameter("id", In = ParameterLocation.Path, Required = true, Type = typeof(string))]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(OkResult))]
        [FunctionName(nameof(Categories) + nameof(Delete))]
        public async Task<IActionResult> Delete([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "categorias/{id}")]
            HttpRequest req, string id, ILogger log)
        {
            try
            {
                await CategoryService.DeleteCategory(id);
                return new OkResult();
            }
            catch (Domain.Exceptions.GenericException e)
            {
                return new JsonResult(e.Message) {StatusCode = e.StatusCode};
            }
            catch (System.Exception e)
            {
                return new JsonResult(e.Message) {StatusCode = 500};
            }
        }
    }
}