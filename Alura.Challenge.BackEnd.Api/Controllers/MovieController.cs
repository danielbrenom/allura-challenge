using System.Collections.Generic;
using System.Threading.Tasks;
using Alura.Challenge.BackEnd.Functions.Extensions;
using Alura.Challenge.Domain.Interfaces;
using Alura.Challenge.Domain.Models.Requests;
using Alura.Challenge.Domain.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Alura.Challenge.BackEnd.Api.Controllers
{
    [ApiController]
    public class MovieController : Controller
    {
        private IMovieService MovieService { get; }

        public MovieController(IMovieService movieService)
        {
            MovieService = movieService;
        }

        /// <summary>
        /// Gets a movie from the database
        /// </summary>
        /// <param name="id">Id of the movie</param>
        /// <response code="200">Movie found</response>
        [HttpGet("/videos/{id}")]
        [ProducesResponseType(typeof(MovieResponse), 200)]
        [Authorize]
        public async Task<IActionResult> GetMovie(string id)
        {
            var movie = await MovieService.GetMovie(id);
            return new JsonResult(movie.GetAs<MovieResponse>());
        }

        /// <summary>
        /// Gets all movies from the database
        /// </summary>
        /// <param name="query">Name of the movie</param>
        /// <param name="page">Page of the search</param>
        /// <response code="200">List of movies</response>
        [HttpGet("/videos")]
        [ProducesResponseType(typeof(List<MovieResponse>), 200)]
        [Authorize]
        public async Task<IActionResult> GetMovies([FromQuery] string query, [FromQuery] string page)
        {
            var movies = await MovieService.GetMovies(query, page);
            return new JsonResult(movies.GetAs<List<MovieResponse>>());
        }

        /// <summary>
        /// Gets a free list of movies
        /// </summary>
        /// <response code="200">A list of movies</returns>
        [HttpGet("/videos/free")]
        [ProducesResponseType(typeof(List<MovieResponse>), 200)]
        [AllowAnonymous]
        public async Task<IActionResult> GetFreeMovies()
        {
            var movies = await MovieService.GetFreeMovies();
            return new JsonResult(movies.GetAs<List<MovieResponse>>());
        }

        /// <summary>
        /// Adds a movie to the database
        /// </summary>
        /// <param name="movie">Movie data to add</param>
        [HttpPost("/videos")]
        [ProducesResponseType(typeof(MovieResponse), 201)]
        [Authorize]
        public async Task<IActionResult> Add([FromBody, BindRequired] MovieRequest movie)
        {
            var response = await MovieService.CreateMovie(movie);
            return new CreatedResult("/videos", response.GetAs<MovieResponse>());
        }

        /// <summary>
        /// Updates a movie in the database
        /// </summary>
        /// <param name="movie">Movie data to update</param>
        /// <param name="id">Id of the movie</param>
        [HttpPatch("/videos/{id}")]
        [ProducesResponseType(typeof(MovieResponse), 200)]
        [Authorize]
        public async Task<IActionResult> Update([FromBody, BindRequired] MovieRequest movie, string id)
        {
            var updatedMovie = await MovieService.UpdateMovie(movie, id);
            return new JsonResult(updatedMovie.GetAs<MovieResponse>()) { StatusCode = 200 };
        }

        /// <summary>
        /// Deletes a movie from the database
        /// </summary>
        /// <param name="id">Id of the movie</param>
        [HttpDelete("/videos/{id}")]
        [Authorize]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Delete(string id)
        {
            await MovieService.DeleteMovie(id);
            return new OkResult();
        }
    }
}