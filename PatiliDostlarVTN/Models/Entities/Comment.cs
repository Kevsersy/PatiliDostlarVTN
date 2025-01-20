namespace PatiliDostlarVTN.Models.Entities
{
    public class Comment : NamedEntity
    {
        public string? AvatarUrl { get; set; } 
        public string? TimeAgo { get; set; } 
        public string? Message { get; set; } 
        public DateTime CreatedAt { get; set; }
    }
}
