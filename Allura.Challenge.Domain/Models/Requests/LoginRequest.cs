namespace Alura.Challenge.Domain.Models.Requests
{
    public class LoginRequest
    {
        /// <summary>
        /// Email do usuario
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Senha do usuario
        /// </summary>
        public string Password { get; set; }
    }
}