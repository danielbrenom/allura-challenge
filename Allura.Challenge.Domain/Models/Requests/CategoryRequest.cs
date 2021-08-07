using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Alura.Challenge.Domain.Models.Requests
{
    public class CategoryRequest
    {
        /// <summary>
        /// Titulo da categoria
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