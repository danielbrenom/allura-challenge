﻿using Newtonsoft.Json;

namespace Allura.Challenge.Domain.Models.Requests
{
    public class MovieRequest
    {
        [JsonProperty("titulo")] public string Title { get; set; }
        [JsonProperty("descricao")] public string Description { get; set; }
        [JsonProperty("url")] public string Url { get; set; }
        [JsonProperty("categoria")] public string Category { get; set; }
    }
}