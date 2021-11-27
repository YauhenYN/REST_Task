using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CardsController : ControllerBase
    {

        private readonly ILogger<CardsController> _logger;
        private readonly Services.IFileBridge<Models.InformationCard> _fileBridge;

        public CardsController(ILogger<CardsController> logger, Services.IFileBridge<Models.InformationCard> fileBridge)
        {
            _logger = logger;
            _fileBridge = fileBridge;
        }


        //CRUD
        [Route("{cardNumber:int?}")]
        [HttpGet]
        public IEnumerable<Models.InformationCard> Read(int? cardNumber)
        {
            if (cardNumber == null)
            {
                return _fileBridge.Contexts;
            }
            else
            {
                return new Models.InformationCard[] { _fileBridge.Contexts.Count > cardNumber ?  _fileBridge.Contexts[cardNumber.Value] : new Models.InformationCard() { Name = "NULL", Img = "NULL" } };
            }
        }

        [HttpPost]
        public bool Create(Models.InformationCard card)
        {
            if (card != null)
            {
                _fileBridge.Contexts.Add(card);
                return true;
            }
            return false;
        }

        [HttpPut]
        public bool Update(Models.InformationCard card)
        {
            if (card != null)
            {
                var selectedCards = _fileBridge.Contexts.Where(e => e.Name.Equals(card.Name)).ToArray();
                foreach (var selectedCard in selectedCards) selectedCard.Img = card.Img;
                if (selectedCards.Length == 0) return false;
                return true;
            }
            return false;
        }

        [HttpDelete]
        public bool Delete(string cardName)
        {
            if (cardName != null)
            {
                if (_fileBridge.Contexts.RemoveAll(e => e.Name.Equals(cardName)) == 0) return false;
                return true;
            }
            return false;
        }
    }
}
