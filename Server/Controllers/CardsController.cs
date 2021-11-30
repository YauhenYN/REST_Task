using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Dtos.InformationCard;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CardsController : ControllerBase
    {

        private readonly ILogger<CardsController> _logger;
        private readonly List<Models.InformationCard> _cards;

        public CardsController(ILogger<CardsController> logger, Services.IFileBridge<Models.InformationCard> fileBridge)
        {
            _logger = logger;
            _cards = fileBridge.Contexts;
        }


        //CRUD
        [HttpGet]
        public ActionResult<Models.InformationCard[]> ReadAll()
        {
            if (_cards.Count > 0) return _cards.ToArray();
            return NotFound();
        }

        [Route("{cardName}")]
        [HttpGet]
        public ActionResult<Models.InformationCard[]> Read(string cardName)
        {
            var cards = _cards.Where(card => card.Name == cardName).ToArray();
            if (cards.Length > 0) return cards;
            return NotFound();
        }

        [HttpPost]
        public ActionResult Create(Models.InformationCard card)
        {
            _cards.Add(card);
            return CreatedAtAction(nameof(Read), new { cardName = card.Name }, card);
        }

        [HttpPut]
        public ActionResult Update(Models.InformationCard card)
        {
            var selectedCards = _cards.Where(e => e.Name.Equals(card.Name)).ToArray();
            foreach (var selectedCard in selectedCards) selectedCard.Img = card.Img;
            if (selectedCards.Length == 0) return NotFound();
            return Ok();
        }

        [HttpDelete]
        public ActionResult Delete(string cardName)
        {
            if (_cards.RemoveAll(e => e.Name.Equals(cardName)) == 0) return NotFound();
            return Ok();
        }
    }
}
