using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Alura.Challenge.Domain.Models.Requests
{
    public class UserRequest
    {
        /// <summary>
        /// Nome do usuário
        /// </summary>
        [JsonPropertyName("nome")]
        [JsonProperty("nome")]
        public string Name { get; set; }
        /// <summary>
        /// Email do usuário
        /// </summary>
        [JsonPropertyName("email")]
        [JsonProperty("email")]
        public string Email { get; set; }
        /// <summary>
        /// Senha do usuário
        /// </summary>
        [JsonPropertyName("senha")]
        [JsonProperty("senha")]
        public string Password { get; set; }
    }
}