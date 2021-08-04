using Alura.Challenge.Database.Models;
using Alura.Challenge.Database.Repositories.Base;
using Alura.Challenge.Database.Repositories.Interfaces;

namespace Alura.Challenge.Database.Repositories
{
    public class GrantTokenRepository : BaseRepository<GrantToken>, IGrantTokenRepository<GrantToken>
    {
        protected override string CollectionName => "GrantTokens";
        public GrantTokenRepository(IConnectionSettings connectionSettings) : base(connectionSettings)
        {
        }

    }
}