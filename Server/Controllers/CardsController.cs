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
        public ActionResult<InformationCardDto[]> ReadAll()
        {
            if (_cards.Count > 0) return _cards.ToDtosBase64Img_UTF8().ToArray();
            return NotFound();
        }

        [Route("{cardName}")]
        [HttpGet]
        public ActionResult<InformationCardDto[]> Read(string cardName)
        {
            var cards = _cards.Where(card => card.Name == cardName).ToList();
            if (cards.Count > 0) return cards.ToDtosBase64Img_UTF8().ToArray();
            return NotFound();
        }

        [HttpPost]
        public ActionResult<InformationCardDto> Create(CreateInformationCardDto dto)
        {
            dto = dto.FromBase64Img_UTF8();
            var card = dto.ToInformationCard();
            _cards.Add(card);
            return CreatedAtAction(nameof(Read), new { cardName = card.Name }, card.ToDtoBase64Img_UTF8());
        }

        [HttpPut("cardName")]
        public ActionResult Update(string cardName, UpdateInformationCardDto dto)
        {
            dto = dto.FromBase64Img_UTF8();
            var selectedCards = _cards.Where(e => e.Name.Equals(cardName)).ToArray();
            if (selectedCards.Length == 0) return NotFound();
            foreach (var selectedCard in selectedCards) { selectedCard.Name = dto.Name; selectedCard.Img = dto.Img; }
            return NoContent();
        }

        [HttpDelete("cardName")]
        public ActionResult Delete(string cardName)
        {
            if (_cards.RemoveAll(e => e.Name.Equals(cardName)) == 0) return NotFound();
            return NoContent();
        }
    }
}
