using System.Collections.Generic;
using Newtonsoft.Json;

namespace Allura.Challenge.Domain.Models.Responses
{
    public class MovieByCategoryResponse
    {
        [JsonProperty("videos")]
        public List<MovieResponse> Movies { get; set; }
    }
}