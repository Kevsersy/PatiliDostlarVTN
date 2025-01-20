using PatiliDostlarVTN.Models.Entities;

namespace PatiliDostlarVTN.Service
{
    public interface IContactService
    {
        bool ValidateContact(Contact contact, out string errorMessage);
        void AddContact(Contact contact);
        List<string> GetUnavailableTimes(DateTime date);
    }

}
