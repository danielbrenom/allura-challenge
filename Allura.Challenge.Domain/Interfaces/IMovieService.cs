using System.Collections.Generic;
using System.Threading.Tasks;
using Allura.Challenge.Domain.Models.Data;
using Allura.Challenge.Domain.Models.Requests;

namespace Allura.Challenge.Domain.Interfaces
{
    public interface IMovieService
    {
        Task<Movie> GetMovie(string id);
        Task<List<Movie>> GetMovies(string page);
        Task<List<Movie>> GetMovies(string query, string page);
        Task<List<Movie>> GetMoviesByCategory(string category, string page);
        Task<Movie> CreateMovie(MovieRequest movieRequest);
        Task<Movie> UpdateMovie(MovieRequest movieRequest, string id);
        Task<bool> DeleteMovie(string id);
    }
}