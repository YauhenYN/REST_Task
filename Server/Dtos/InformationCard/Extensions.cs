using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace Server.Dtos.InformationCard
{
    public static class Extensions
    {
        public static InformationCardDto ToDtoBase64Img_UTF8(this Models.InformationCard card) => new InformationCardDto() { Name = card.Name, Img = Convert.ToBase64String(card.Img) };
        public static IEnumerable<InformationCardDto> ToDtosBase64Img_UTF8(this IEnumerable<Models.InformationCard> cards) => cards.Select(card => card.ToDtoBase64Img_UTF8());
        public static Models.InformationCard ToInformationCardFromBase64(this CreateInformationCardDto dto) => new Models.InformationCard() { Name = dto.Name, Img = Convert.FromBase64String(dto.Img) };
        public static Models.InformationCard ToInformationCardFromBase64(this UpdateInformationCardDto dto) => new Models.InformationCard() { Name = dto.Name, Img = Convert.FromBase64String(dto.Img) };
        //client - dto(stringBase64) - informationcard(byte[]) - file (stringBase64)
    }
}
