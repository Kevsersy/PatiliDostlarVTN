using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using PatiliDostlarVTN.Models.Entities;

namespace PatiliDostlarVTN.Service
{
    public class ContactService : IContactService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ContactService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public void AddContact(Contact contact)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Contact>> GetContactsAsync()
        {
            var client = _httpClientFactory.CreateClient("PatiliDost");
            var response = await client.GetAsync("/Contacts");

            if (!response.IsSuccessStatusCode)
            {
                return new List<Contact>(); 
            }

            var jsonData = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Contact>>(jsonData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public List<string> GetUnavailableTimes(DateTime date)
        {
            throw new NotImplementedException();
        }

        public bool ValidateContact(Contact contact, out string errorMessage)
        {
            throw new NotImplementedException();
        }
    }
}
