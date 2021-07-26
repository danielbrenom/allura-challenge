using System.Collections.Generic;
using System.Threading.Tasks;
using Allura.Challenge.Domain.Models.Data;
using Allura.Challenge.Domain.Models.Requests;

namespace Allura.Challenge.Domain.Interfaces
{
    public interface IMovieService
    {
        Task<Movie> GetMovie(string id);
        Task<List<Movie>> GetMovies();
        Task<List<Movie>> GetMovies(string query);
        Task<List<Movie>> GetMoviesByCategory(string category);
        Task<Movie> CreateMovie(MovieRequest movieRequest);
        Task<Movie> UpdateMovie(MovieRequest movieRequest, string id);
        Task<bool> DeleteMovie(string id);
    }
}