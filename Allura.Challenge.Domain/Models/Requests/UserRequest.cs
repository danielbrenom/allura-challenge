namespace Alura.Challenge.Domain.Models.Requests
{
    public class UserRequest
    {
        /// <summary>
        /// Nome do usuário
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Email do usuário
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Senha do usuário
        /// </summary>
        public string Password { get; set; }
    }
}