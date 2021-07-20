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
        private IValidator<Movie> Validator { get; }

        public MovieService(IMovieRepository<Database.Models.Movie> movieRepository, IValidator<Movie> validator)
        {
            MovieRepository = movieRepository;
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
            var validationResult = await Validator.CheckAsync(movie);
            if (!validationResult.IsValid)
                throw new InvalidDataException("Movie data is invalid", validationResult.Errors);
            movie.Id = System.Guid.NewGuid().ToString("N");
            var result = await MovieRepository.AddAsync(movie.GetAs<Database.Models.Movie>());
            if (!result)
                throw new System.Exception("An error occurred creating the movie");
            return movie;
        }

        public async Task<Movie> UpdateMovie(MovieRequest movieRequest)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> DeleteMovie(MovieRequest movieRequest)
        {
            throw new System.NotImplementedException();
        }
    }
}