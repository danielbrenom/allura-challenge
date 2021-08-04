using System.Threading.Tasks;
using Alura.Challenge.Domain.Models.Requests;
using Alura.Challenge.Domain.Models.Responses;

namespace Alura.Challenge.Domain.Interfaces
{
    public interface ILoginService
    {
        public Task<LoginResponse> LoginUser(LoginRequest user);
    }
}