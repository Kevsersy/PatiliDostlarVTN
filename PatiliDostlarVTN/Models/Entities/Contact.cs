using System.Text.Json.Serialization;

namespace PatiliDostlarVTN.Models.Entities;

public class Contact : ContactableEntity
{
    [JsonPropertyName("Company")]
    public string? Company { get; set; }
    [JsonPropertyName("AppointmentDate")]
    public DateTime AppointmentDate { get; set; }
    [JsonPropertyName("AppointmentTime")]
    public string? AppointmentTime { get; set; }
    
}
