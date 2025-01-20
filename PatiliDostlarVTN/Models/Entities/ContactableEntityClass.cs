namespace PatiliDostlarVTN.Models.Entities
{
    public abstract class ContactableEntity : NamedEntity
    {
        public string? Email { get; set; }
        public string? Phone { get; set; }

    }
}
