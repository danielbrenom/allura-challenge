using Newtonsoft.Json;

namespace Allura.Challenge.Domain.Models.Responses
{
    public class CategoryResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("titulo")]
        public string Title { get; set; }
        [JsonProperty("cor")]
        public string Color { get; set; }
    }
}