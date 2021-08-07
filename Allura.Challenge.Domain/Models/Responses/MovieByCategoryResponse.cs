using System.Collections.Generic;
using Newtonsoft.Json;

namespace Alura.Challenge.Domain.Models.Responses
{
    public class MovieByCategoryResponse
    {
        /// <summary>
        /// Videos pertencentes a categoria
        /// </summary>
        [JsonProperty("videos")]
        public List<MovieResponse> Movies { get; set; }
    }
}