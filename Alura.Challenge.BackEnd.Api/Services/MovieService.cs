using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alura.Challenge.BackEnd.Functions.Extensions;
using Alura.Challenge.Database.Repositories.Base;
using Alura.Challenge.Database.Repositories.Interfaces;
using Alura.Challenge.Domain.Exceptions;
using Alura.Challenge.Domain.Interfaces;
using Alura.Challenge.Domain.Models.Data;
using Alura.Challenge.Domain.Models.Requests;
using Alura.Challenge.Domain.Validators.Interfaces;

namespace Alura.Challenge.BackEnd.Api.Services
{
    public class MovieService : IMovieService
    {
        private IMovieRepository<Database.Models.Movie> MovieRepository { get; }
        private ICategoryRepository<Database.Models.Category> CategoryRepository { get; }
        private IValidator<Movie> Validator { get; }

        public MovieService(IMovieRepository<Database.Models.Movie> movieRepository, ICategoryRepository<Database.Models.Category> categoryRepository, IValidator<Movie> validator)
        {
            MovieRepository = movieRepository;
            CategoryRepository = categoryRepository;
            Validator = validator;
        }


        public async Task<Movie> GetMovie(string id)
        {
            var result = await MovieRepository.GetAsync(id);
            if (result is null)
                throw new GenericException("Movie not found", 404);
            return result.GetAs<Movie>();
        }

        public async Task<List<Movie>> GetMovies(string page)
        {
            var paginator = new BasePaginator(5) {Page = string.IsNullOrEmpty(page) ? 1 : int.Parse(page)};
            var result = await MovieRepository.GetAllAsync(paginator);
            return result.ToList().GetAs<List<Movie>>();
        }

        public async Task<List<Movie>> GetMovies(string query, string page)
        {
            if (string.IsNullOrEmpty(query))
                return await GetMovies(page);

            var paginator = new BasePaginator(5) {Page = string.IsNullOrEmpty(page) ? 1 : int.Parse(page)};

            var result = await MovieRepository.GetAllAsync(m => m.Title.ToLowerInvariant().Contains(query.ToLowerInvariant()), paginator);
            return result.ToList().GetAs<List<Movie>>();
        }
        
        public async Task<List<Movie>> GetMoviesByCategory(string category, string page)
        {
            var paginator = new BasePaginator(5) {Page = string.IsNullOrEmpty(page) ? 1 : int.Parse(page)};
            var result = await MovieRepository.GetAllAsync(m => m.Category.Id.Equals(category), paginator);
            return result.ToList().GetAs<List<Movie>>();
        }

        public async Task<Movie> CreateMovie(MovieRequest movieRequest)
        {
            var movie = movieRequest.GetAs<Movie>();
            Database.Models.Category category;
            if (string.IsNullOrEmpty(movieRequest.Category))
            {
                category = await CategoryRepository.GetDefaultCategory();
            }
            else
            {
                category = await CategoryRepository.GetAsync(movieRequest.Category);
            }

            movie.Category = category.GetAs<Category>();
            var validationResult = await Validator.CheckAsync(movie, "Create");
            if (!validationResult.IsValid)
                throw new InvalidDataException("Movie data is invalid", validationResult.Errors);
            movie.Id = System.Guid.NewGuid().ToString("N");
            var result = await MovieRepository.AddAsync(movie.GetAs<Database.Models.Movie>());
            if (!result)
                throw new System.Exception("An error occurred creating the movie");
            return movie;
        }

        public async Task<Movie> UpdateMovie(MovieRequest movieRequest, string id)
        {
            var movie = movieRequest.GetAs<Movie>();
            if (!string.IsNullOrEmpty(movieRequest.Category))
            {
                var category = await CategoryRepository.GetAsync(movieRequest.Category);
                movie.Category = category.GetAs<Category>();
            }

            var validationResult = await Validator.CheckAsync(movie, "Update");
            if (!validationResult.IsValid)
                throw new InvalidDataException("Movie data is invalid", validationResult.Errors);
            movie.Id = id;
            var result = await MovieRepository.UpdateAsync(movie.GetAs<Database.Models.Movie>());
            if (!result)
                throw new System.Exception("An error occurred updating the movie");
            return movie;
        }

        public async Task<bool> DeleteMovie(string id)
        {
            var movie = await MovieRepository.GetAsync(id);
            if (movie is null)
                throw new GenericException("Movie not found", 404);
            var result = await MovieRepository.DeleteAsync(movie);
            if (!result)
                throw new GenericException("Could not delete movie", 500);
            return true;
        }
    }
}