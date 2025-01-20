namespace PatiliDostlarVTN.Models.Entities
{
    public class Work : BaseEntity
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? WorkType { get; set; }
        public string? DetailLink { get; set; }
    }
}
