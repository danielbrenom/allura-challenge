namespace Alura.Challenge.Domain.Models.Responses
{
    public class LoginResponse
    {
        /// <summary>
        /// Identificador do usuario
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Nome do usuario
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Email do usuario
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Token JWT do usuario
        /// </summary>
        public string Token { get; set; }
    }
}