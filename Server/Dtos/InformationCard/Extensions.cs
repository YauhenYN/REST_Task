using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Dtos.InformationCard
{
    public static class Extensions
    {
        public static IEnumerable<InformationCardDto> ToDtos(this List<Models.InformationCard> cards) => cards.Select( card => new InformationCardDto{ Name = card.Name, Img = card.Img });
        public static Models.InformationCard ToInformationCard(this CreateInformationCardDto dto) => new Models.InformationCard() { Name = dto.Name, Img = dto.Img };
    }
}
