using Alura.Challenge.Database.Models;
using Alura.Challenge.Database.Repositories.Base;
using Alura.Challenge.Database.Repositories.Interfaces;

namespace Alura.Challenge.Database.Repositories
{
    public class MovieRepository : BaseRepository<Movie>, IMovieRepository<Movie>
    {
        protected override string CollectionName => "Movies";

        public MovieRepository(IConnectionSettings connectionSettings) : base(connectionSettings)
        {
        }
    }
}