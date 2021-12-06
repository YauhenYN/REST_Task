using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Dtos;

namespace Client.Web
{
    public class InformationCards_Client : ICRUDAsync<string, InformationCardDto>
    {
        private HTTPClient _client;
        public InformationCards_Client(string url)
        {
            _client = new HTTPClient(url + "Cards/");
        }

        public async Task CreateAsync(InformationCardDto entity)
        {
            string cardString = JsonConvert.SerializeObject(entity);
            string response = await _client.SendAsync("", RequestMethod.POST, cardString, System.Net.HttpStatusCode.Created);
        }

        public async Task<IEnumerable<InformationCardDto>> ReadAsync(string cardName)
        {
            string response = await _client.SendAsync(cardName, RequestMethod.GET, "", System.Net.HttpStatusCode.OK);
            IEnumerable<InformationCardDto> cards = JsonConvert.DeserializeObject<IEnumerable<InformationCardDto>>(response);
            return cards;
        }

        public async Task<IEnumerable<InformationCardDto>> ReadAllAsync()
        {
            string response = await _client.SendAsync("", RequestMethod.GET, "", System.Net.HttpStatusCode.OK);
            IEnumerable<InformationCardDto> cards = JsonConvert.DeserializeObject<IEnumerable<InformationCardDto>>(response);
            return cards;
        }

        public async Task RemoveAllAsync(string cardName)
        {
            string response = await _client.SendAsync("cardName?cardName=" + cardName, RequestMethod.DELETE, "", System.Net.HttpStatusCode.NoContent);
        }

        public async Task UpdateAllAsync(string cardName, InformationCardDto entity)
        {
            string cardString = JsonConvert.SerializeObject(entity);
            string response = await _client.SendAsync("cardName?cardName=" + cardName, RequestMethod.PUT, cardString, System.Net.HttpStatusCode.NoContent);
        }
    }
}
