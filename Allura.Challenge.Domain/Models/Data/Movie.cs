namespace Alura.Challenge.Domain.Models.Data
{
    public class Movie
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public Category Category { get; set; }
    }
}