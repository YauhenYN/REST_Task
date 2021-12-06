using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Dtos
{
    public class InformationCardDto
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("img")]
        public string Img { get; set; }
    }
}
