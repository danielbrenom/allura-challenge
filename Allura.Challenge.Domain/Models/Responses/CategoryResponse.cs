using Newtonsoft.Json;

namespace Alura.Challenge.Domain.Models.Responses
{
    public class CategoryResponse
    {
        /// <summary>
        /// Identificador da categoria
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }
        /// <summary>
        /// Nome da categoria
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