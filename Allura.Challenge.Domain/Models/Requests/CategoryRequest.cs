using Newtonsoft.Json;

namespace Alura.Challenge.Domain.Models.Requests
{
    public class CategoryRequest
    {
        [JsonProperty("titulo")]
        public string Title { get; set; }
        [JsonProperty("cor")]
        public string Color { get; set; }
    }
}