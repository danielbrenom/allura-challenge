using Newtonsoft.Json;

namespace Alura.Challenge.Domain.Models.Requests
{
    public class MovieRequest
    {
        /// <summary>
        /// Titulo do video
        /// </summary>
        [JsonProperty("titulo")] public string Title { get; set; }
        /// <summary>
        /// Descrição do video
        /// </summary>
        [JsonProperty("descricao")] public string Description { get; set; }
        /// <summary>
        /// URL do video
        /// </summary>
        [JsonProperty("url")] public string Url { get; set; }
        /// <summary>
        /// Categoria do video
        /// </summary>
        [JsonProperty("categoriaId")] public string Category { get; set; }
    }
}