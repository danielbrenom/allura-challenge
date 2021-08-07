using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Alura.Challenge.Domain.Models.Responses
{
    public class CategoryResponse
    {
        /// <summary>
        /// Identificador da categoria
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }
        /// <summary>
        /// Nome da categoria
        /// </summary>
        [JsonPropertyName("titulo")]
        [JsonProperty("titulo")]
        public string Title { get; set; }
        /// <summary>
        /// Cor da categoria
        /// </summary>
        [JsonPropertyName("cor")]
        [JsonProperty("cor")]
        public string Color { get; set; }
    }
}