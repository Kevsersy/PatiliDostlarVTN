using PatiliDostlarVTN.Models.Entities;
using PatiliDostlarVTN.Models;

namespace PatiliDostlarVTN.Service
{
    public class ContactService : IContactService
    {
        private readonly PatiDostumContext _context;

        public ContactService(PatiDostumContext context)
        {
            _context = context;
        }

        public bool ValidateContact(Contact contact, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (contact.AppointmentDate.Date < DateTime.Now.Date)
            {
                errorMessage = "Geçmiş bir tarih seçemezsiniz. Lütfen geçerli bir tarih seçin.";
                return false;
            }

            if (contact.AppointmentDate.Date > DateTime.Now.Date.AddYears(1))
            {
                errorMessage = "Randevu tarihi bir yıl içinde olmalıdır. Lütfen geçerli bir tarih seçin.";
                return false;
            }

           

            if (_context.Contacts.Any(c =>
                c.AppointmentDate.Date == contact.AppointmentDate.Date &&
                c.AppointmentTime == contact.AppointmentTime))
            {
                errorMessage = "Seçilen tarih ve saatte zaten bir randevu bulunmaktadır. Lütfen başka bir saat seçin.";
                return false;
            }

            return true;
        }

        public void AddContact(Contact contact)
        {
            _context.Contacts.Add(contact);
            _context.SaveChanges();
        }

        public List<string> GetUnavailableTimes(DateTime date)
        {
            return _context.Contacts
                .Where(c => c.AppointmentDate.Date == date.Date)
                .Select(c => c.AppointmentTime)
                .ToList();
        }
    }

}
