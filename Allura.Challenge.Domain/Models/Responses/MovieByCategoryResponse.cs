using System.Collections.Generic;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Alura.Challenge.Domain.Models.Responses
{
    public class MovieByCategoryResponse
    {
        /// <summary>
        /// Videos pertencentes a categoria
        /// </summary>
        [JsonPropertyName("videos")]
        [JsonProperty("videos")]
        public List<MovieResponse> Movies { get; set; }
    }
}