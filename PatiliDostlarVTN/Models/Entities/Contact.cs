namespace PatiliDostlarVTN.Models.Entities
{
    public class Contact : ContactableEntity
    {

        public string? Company { get; set; }

        public DateTime AppointmentDate { get; set; } 
        public string? AppointmentTime { get; set; }
    }
}
