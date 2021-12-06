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
        private readonly Services.IFileBridge<Models.InformationCard> _bridge;

        public CardsController(ILogger<CardsController> logger, Services.IFileBridge<Models.InformationCard> fileBridge)
        {
            _logger = logger;
            _bridge = fileBridge;
        }


        //CRUD
        [HttpGet]
        public async Task<ActionResult<InformationCardDto[]>> ReadAllAsync()
        {
            if (_bridge.Entities.Count > 0) return (await _bridge.GetAllAsync()).ToDtosBase64Img_UTF8().ToArray();
            else return NotFound();
        }

        [Route("{cardName}")]
        [HttpGet]
        public async Task<ActionResult<InformationCardDto[]>> ReadAsync(string cardName)
        {
            var cards = await _bridge.GetAsync(card => card.Name.Equals(cardName));
            if (cards.Count() > 0) return cards.ToDtosBase64Img_UTF8().ToArray();
            else return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<InformationCardDto>> CreateAsync(CreateInformationCardDto dto)
        {
            var card = dto.ToInformationCardFromBase64();
            await _bridge.CreateAsync(card);
            return CreatedAtAction(nameof(ReadAsync), new { cardName = card.Name }, card.ToDtoBase64Img_UTF8());
        }

        [HttpPut("cardName")]
        public async Task<ActionResult> UpdateAsync(string cardName, UpdateInformationCardDto dto)
        {
            if (!_bridge.Entities.Any(card => card.Name.Equals(cardName))) return NotFound();
            await _bridge.UpdateAllAsync(card => card.Name.Equals(cardName), dto.ToInformationCardFromBase64());
            return NoContent();
        }

        [HttpDelete("cardName")]
        public async Task<ActionResult> DeleteAsync(string cardName)
        {
            if (!_bridge.Entities.Any(card => card.Name.Equals(cardName))) return NotFound();
            await _bridge.RemoveAllAsync(card => card.Name.Equals(cardName));
            return NoContent();
        }
    }
}
