using Alura.Challenge.Database.Models;
using Alura.Challenge.Database.Repositories.Base;
using Alura.Challenge.Database.Repositories.Interfaces;

namespace Alura.Challenge.Database.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository<User>
    {
        protected override string CollectionName => "Users";

        public UserRepository(IConnectionSettings connectionSettings) : base(connectionSettings)
        {
        }
    }
}