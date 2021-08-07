using Newtonsoft.Json;

namespace Alura.Challenge.Domain.Models.Requests
{
    public class CategoryRequest
    {
        /// <summary>
        /// Titulo da categoria
        /// </summary>
        [JsonProperty("titulo")]
        public string Title { get; set; }
        /// <summary>
        /// Cor da categoria
        /// </summary>
        [JsonProperty("cor")]
        public string Color { get; set; }
    }
}