using Client.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Web
{
    public class InformationCards_Client : ICRUDAsync<string, InformationCard>
    {
        private HTTPClient _client;
        public InformationCards_Client(string url)
        {
            _client = new HTTPClient(url + "Cards/");
        }

        public async Task CreateAsync(InformationCard entity)
        {
            entity.Img = entity.Img.ToBase64_UTF8();
            string cardString = JsonConvert.SerializeObject(entity);
            string response = await _client.SendAsync("", RequestMethod.POST, cardString, System.Net.HttpStatusCode.Created);
        }

        public async Task<IEnumerable<InformationCard>> ReadAsync(string cardName)
        {
            string response = await _client.SendAsync(cardName, RequestMethod.GET, "", System.Net.HttpStatusCode.OK);
            IEnumerable<InformationCard> cards = JsonConvert.DeserializeObject<IEnumerable<InformationCard>>(response);
            foreach (var card in cards) card.Img = card.Img.FromBase64_UTF8();
            return cards;
        }

        public async Task<IEnumerable<InformationCard>> ReadAllAsync()
        {
            string response = await _client.SendAsync("", RequestMethod.GET, "", System.Net.HttpStatusCode.OK);
            IEnumerable<InformationCard> cards = JsonConvert.DeserializeObject<IEnumerable<InformationCard>>(response);
            foreach (var card in cards) card.Img = card.Img.FromBase64_UTF8();
            return cards;
        }

        public async Task RemoveAllAsync(string cardName)
        {
            string response = await _client.SendAsync("cardName?cardName=" + cardName, RequestMethod.DELETE, "", System.Net.HttpStatusCode.NoContent);
        }

        public async Task UpdateAllAsync(string cardName, InformationCard entity)
        {
            entity.Img = entity.Img.ToBase64_UTF8();
            string cardString = JsonConvert.SerializeObject(entity);
            string response = await _client.SendAsync("cardName?cardName=" + cardName, RequestMethod.PUT, cardString, System.Net.HttpStatusCode.NoContent);
        }
    }
}
