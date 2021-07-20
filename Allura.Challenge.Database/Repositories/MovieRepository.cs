using Allura.Challenge.Database.Models;
using Allura.Challenge.Database.Repositories.Base;
using Allura.Challenge.Database.Repositories.Interfaces;

namespace Allura.Challenge.Database.Repositories
{
    public class MovieRepository : BaseRepository<Movie>, IMovieRepository<Movie>
    {
        protected override string CollectionName => "Movies";

        public MovieRepository(IConnectionSettings connectionSettings) : base(connectionSettings)
        {
        }
    }
}