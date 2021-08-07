using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Alura.Challenge.Domain.Models.Responses
{
    public class LoginResponse
    {
        /// <summary>
        /// Identificador do usuario
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }
        /// <summary>
        /// Nome do usuario
        /// </summary>
        [JsonProperty("nome")]
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