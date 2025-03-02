using System.Collections.Generic;
using PatiliDostlarVTN.Models.Entities;

namespace PatiliDostlarVTN.ViewModels
{
    public class ContactVM
    {
        public List<Contact> ContactList { get; set; } = new List<Contact>(); // Mevcut randevular
        public Contact NewContact { get; set; } = new Contact(); // Yeni randevu formu için boş model
    }
}
