﻿using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Allura.Challenge.BackEnd.Extensions;
using Allura.Challenge.Domain.Interfaces;
using Allura.Challenge.Domain.Models.Data;
using Allura.Challenge.Domain.Models.Requests;
using Allura.Challenge.Domain.Models.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace Allura.Challenge.BackEnd.Routes
{
    public class Movies
    {
        private IMovieService MovieService { get; }

        public Movies(IMovieService movieService)
        {
            MovieService = movieService;
        }

        [OpenApiOperation("Get Movie", "Get", Summary = "Get movie", Description = "Gets a movie from the database", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiParameter("id", In = ParameterLocation.Path, Required = true, Type = typeof(string))]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(MovieResponse))]
        [FunctionName(nameof(Get))]
        public async Task<IActionResult> Get([HttpTrigger(AuthorizationLevel.Function, "get", Route = "videos/{id}")]
            HttpRequest req, string id, ILogger log)
        {
            try
            {
                var movie = await MovieService.GetMovie(id);
                return new JsonResult(movie.GetAs<MovieResponse>());
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
        
        [OpenApiOperation("Get Movies", "Get", Summary = "Get movies", Description = "Gets all movies from the database", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(List<MovieResponse>))]
        [FunctionName(nameof(GetAll))]
        public async Task<IActionResult> GetAll([HttpTrigger(AuthorizationLevel.Function, "get", Route = "videos")]
            HttpRequest req, ILogger log)
        {
            try
            {
                var movie = await MovieService.GetMovies();
                return new JsonResult(movie.GetAs<List<MovieResponse>>());
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


        [OpenApiOperation("Add Movie", "Add", Summary = "Add movie", Description = "Adds a new movie to the database", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiRequestBody("application/json", typeof(MovieRequest))]
        [OpenApiResponseWithBody(HttpStatusCode.Created, "application/json", typeof(MovieResponse))]
        [FunctionName(nameof(Add))]
        public async Task<IActionResult> Add([HttpTrigger(AuthorizationLevel.Function, "post", Route = "videos")]
            HttpRequest req, ILogger log)
        {
            try
            {
                var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var data = JsonConvert.DeserializeObject<MovieRequest>(requestBody);
                var movie = await MovieService.CreateMovie(data);
                return new CreatedResult("/videos", movie.GetAs<MovieResponse>());
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
    }
}