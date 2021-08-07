using System.Threading.Tasks;
using Alura.Challenge.Domain.Exceptions;
using Alura.Challenge.Domain.Interfaces;
using Alura.Challenge.Domain.Models.Requests;
using Alura.Challenge.Domain.Models.Responses;

namespace Alura.Challenge.BackEnd.Api.Services
{
    public class LoginService : ILoginService
    {
        private IUserService UserService { get; }
        private IGrantTokenService GrantTokenService { get; }
        
        public LoginService(IUserService userService, IGrantTokenService grantTokenService)
        {
            UserService = userService;
            GrantTokenService = grantTokenService;
        }

        public async Task<LoginResponse> LoginUser(LoginRequest userRequest)
        {
            var user = await UserService.GetUserByEmail(userRequest.Email);
            if (user is null)
                throw new GenericException("User doesn't exist", 404);
            var result = BCrypt.Net.BCrypt.EnhancedVerify(userRequest.Password, user.Password);
            if (!result)
                throw new GenericException("Invalid credentials", 401);
            var token = GrantTokenService.GenerateToken(user);
            return new LoginResponse
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Token = token
            };
        }
    }
}