namespace PatiliDostlarVTN.Models.Entities
{
    public class FeaturedWork : BaseEntity
    {
        public string? Title { get; set; } 
        public string? Subtitle { get; set; } 
        public string? Description { get; set; } 
        public List<string>? Benefits { get; set; } 
        public string? Footer { get; set; } 
        public List<string>? ImageUrls { get; set; } 
    }
}
