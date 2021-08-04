using System.Security.Claims;
using System.Threading.Tasks;
using Alura.Challenge.BackEnd.Functions.Extensions;
using Alura.Challenge.Database.Models;
using Alura.Challenge.Domain.Interfaces;
using Alura.Challenge.Domain.Models.Requests;
using Alura.Challenge.Domain.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Alura.Challenge.BackEnd.Api.Controllers
{
    [ApiController]
    public class UserController
    {
        private IUserService UserService { get; }
        private IHttpContextAccessor HttpContextAccessor { get; }

        public UserController(IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            UserService = userService;
            HttpContextAccessor = httpContextAccessor;
        }
        
        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="data">The user data for creation</param>
        /// <response code="201">User created</response>
        [HttpPost("/user")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(UserCreatedResponse), 201)]
        public async Task<IActionResult> Create([FromBody] UserRequest data)
        {
            var result = await UserService.CreateUser(data);
            return new JsonResult(result) { StatusCode = 201 };
        }

        /// <summary>
        /// Gets logged user information
        /// </summary>
        [HttpGet("/user/me")]
        [Authorize]
        [ProducesResponseType(typeof(UserCreatedResponse), 200)]
        public IActionResult Get()
        {
            var user = HttpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.UserData)?.Value;
            return new JsonResult(JsonConvert.DeserializeObject<UserCreatedResponse>(user ?? string.Empty));
        }
    }
}