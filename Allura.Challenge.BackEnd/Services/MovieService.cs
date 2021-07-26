using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Allura.Challenge.BackEnd.Extensions;
using Allura.Challenge.Database.Repositories.Interfaces;
using Allura.Challenge.Domain.Exceptions;
using Allura.Challenge.Domain.Interfaces;
using Allura.Challenge.Domain.Models.Data;
using Allura.Challenge.Domain.Models.Requests;
using Allura.Challenge.Domain.Validators.Interfaces;

namespace Allura.Challenge.BackEnd.Services
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

        public async Task<List<Movie>> GetMovies()
        {
            var result = await MovieRepository.GetAllAsync();
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