using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Alura.Challenge.Domain.Models.Responses
{
    public class MovieResponse
    {
        /// <summary>
        /// Identificador do video
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// Titulo do video
        /// </summary>
        [JsonPropertyName("titulo")]
        [JsonProperty("titulo")]
        public string Title { get; set; }

        /// <summary>
        /// Descrição do video
        /// </summary>
        [JsonPropertyName("descricao")]
        [JsonProperty("descricao")]
        public string Description { get; set; }

        /// <summary>
        /// URL do video
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; }

        /// <summary>
        /// Categoria do video
        /// </summary>
        [JsonPropertyName("categoriaId")]
        [JsonProperty("categoriaId")]
        public string Category { get; set; }
    }
}