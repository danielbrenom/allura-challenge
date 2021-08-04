using System.Threading.Tasks;
using Alura.Challenge.Domain.Models.Data;
using Alura.Challenge.Domain.Models.Requests;

namespace Alura.Challenge.Domain.Interfaces
{
    public interface IUserService
    {
        public Task<User> GetUserByEmail(string email);
        public Task<User> CreateUser(UserRequest user);
        public Task<User> UpdateUser(UserRequest user, string id);
        public Task<User> DeleteUser(UserRequest user, string id);
    }
}