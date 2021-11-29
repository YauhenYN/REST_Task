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
        [HttpGet]
        public ActionResult<Models.InformationCard[]> ReadAll()
        {
            if (_fileBridge.Contexts.Count > 0) return _fileBridge.Contexts.ToArray();
            return NotFound();
        }

        [Route("{cardNumber:int}")]
        [HttpGet]
        public ActionResult<Models.InformationCard> Read(int? cardNumber)
        {
            if (_fileBridge.Contexts.Count > cardNumber) return _fileBridge.Contexts[cardNumber.Value];
            return NotFound();
        }

        [HttpPost]
        public ActionResult Create(Models.InformationCard card)
        {
            if (card.Validate())
            {
                _fileBridge.Contexts.Add(card);
                return Ok();
            }
            return ValidationProblem();
        }

        [HttpPut]
        public ActionResult Update(Models.InformationCard card)
        {
            if (card.Validate())
            {
                var selectedCards = _fileBridge.Contexts.Where(e => e.Name.Equals(card.Name)).ToArray();
                foreach (var selectedCard in selectedCards) selectedCard.Img = card.Img;
                if (selectedCards.Length == 0) return NotFound();
                return Ok();
            }
            return ValidationProblem();
        }

        [HttpDelete]
        public ActionResult Delete(string cardName)
        {
            if (cardName != "")
            {
                if (_fileBridge.Contexts.RemoveAll(e => e.Name.Equals(cardName)) == 0) return NotFound();
                return Ok();
            }
            return ValidationProblem();
        }
    }
}
