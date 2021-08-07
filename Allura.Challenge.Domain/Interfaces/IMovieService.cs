using System.Collections.Generic;
using System.Threading.Tasks;
using Alura.Challenge.Domain.Models.Data;
using Alura.Challenge.Domain.Models.Requests;

namespace Alura.Challenge.Domain.Interfaces
{
    public interface IMovieService
    {
        Task<Movie> GetMovie(string id);
        Task<List<Movie>> GetMovies(string page);
        Task<List<Movie>> GetMovies(string query, string page);
        Task<List<Movie>> GetFreeMovies();
        Task<List<Movie>> GetMoviesByCategory(string category, string page);
        Task<Movie> CreateMovie(MovieRequest movieRequest);
        Task<Movie> UpdateMovie(MovieRequest movieRequest, string id);
        Task<bool> DeleteMovie(string id);
    }
}