using System.Threading.Tasks;
using Alura.Challenge.Domain.Interfaces;
using Alura.Challenge.Domain.Models.Requests;
using Alura.Challenge.Domain.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Alura.Challenge.BackEnd.Api.Controllers
{
    [ApiController]
    public class LoginController
    {
        private ILoginService LoginService { get; }

        public LoginController(ILoginService loginService)
        {
            LoginService = loginService;
        }

        /// <summary>
        /// Logs a user in the API
        /// </summary>
        /// <param name="data">User login data</param>
        /// <returns></returns>
        [HttpPost("/login")]
        [ProducesResponseType(typeof(LoginResponse), 200)]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest data)
        {
            var result = await LoginService.LoginUser(data);
            return new JsonResult(result) { StatusCode = 200 };
        }

    }
}