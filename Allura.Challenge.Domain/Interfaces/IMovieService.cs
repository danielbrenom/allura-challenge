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
        Task<Movie> CreateMovie(MovieRequest movieRequest);
        Task<Movie> UpdateMovie(MovieRequest movieRequest);
        Task<bool> DeleteMovie(MovieRequest movieRequest);
    }
}