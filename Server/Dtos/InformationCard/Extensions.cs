using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace Server.Dtos.InformationCard
{
    public static class Extensions
    {
        public static IEnumerable<InformationCardDto> ToDtos(this IEnumerable<Models.InformationCard> cards) => cards.Select(card => new InformationCardDto { Name = card.Name, Img = card.Img });
        public static IEnumerable<InformationCardDto> ToDtosBase64Img_UTF8(this IEnumerable<Models.InformationCard> cards) => cards.Select(card => new InformationCardDto { Name = card.Name, Img = card.Img.ToBase64_UTF8() });
        public static Models.InformationCard ToInformationCard(this CreateInformationCardDto dto) => new Models.InformationCard() { Name = dto.Name, Img = dto.Img };
        public static string ToBase64_UTF8(this string str) => Convert.ToBase64String(Encoding.UTF8.GetBytes(str));
        public static string FromBase64_UTF8(this string str) { Console.WriteLine(str); return Encoding.UTF8.GetString(Convert.FromBase64String(str)); }
        public static CreateInformationCardDto FromBase64Img_UTF8(this CreateInformationCardDto card) => new CreateInformationCardDto() { Name = card.Name, Img = card.Img.FromBase64_UTF8()};
        public static UpdateInformationCardDto FromBase64Img_UTF8(this UpdateInformationCardDto card) => new UpdateInformationCardDto() { Name = card.Name, Img = card.Img.FromBase64_UTF8() };
    }
}
