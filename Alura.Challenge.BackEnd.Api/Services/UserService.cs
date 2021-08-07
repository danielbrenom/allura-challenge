using System.Linq;
using System.Threading.Tasks;
using Alura.Challenge.BackEnd.Functions.Extensions;
using Alura.Challenge.Database.Repositories.Interfaces;
using Alura.Challenge.Domain.Exceptions;
using Alura.Challenge.Domain.Interfaces;
using Alura.Challenge.Domain.Models.Data;
using Alura.Challenge.Domain.Models.Requests;

namespace Alura.Challenge.BackEnd.Api.Services
{
    public class UserService : IUserService
    {
        private IUserRepository<Database.Models.User> UserRepository { get; }

        public UserService(IUserRepository<Database.Models.User> userRepository)
        {
            UserRepository = userRepository;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var result = await UserRepository.GetAllAsync(u => u.Email.Equals(email));
            return result.FirstOrDefault().GetAs<User>();
        }

        public async Task<User> CreateUser(UserRequest user)
        {
            var searchUser = await GetUserByEmail(user.Email);
            if (searchUser != null)
                throw new InvalidDataException("User email already exists", null, null);
            var newUser = new User
            {
                Id = System.Guid.NewGuid().ToString("N"),
                Name = user.Name,
                Email = user.Email,
                Password = BCrypt.Net.BCrypt.EnhancedHashPassword(user.Password),
            };
            var result = await UserRepository.AddAsync(newUser.GetAs<Database.Models.User>());
            return result ? newUser : default;
        }

        public async Task<User> UpdateUser(UserRequest user, string id)
        {
            return default;
        }

        public async Task<User> DeleteUser(UserRequest user, string id)
        {
            return default;
        }
    }
}