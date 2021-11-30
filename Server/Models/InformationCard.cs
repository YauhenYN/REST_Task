using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models
{
    public class InformationCard : IEntity
    {
        public string Name { get; set; }
        public string Img { get; set; }
    }
    //img
    //file(base64) => application.set(base64:IFileBridge)
    //user(base64) => application.set(base64)
    //user(base64) <= application.get(base64)
    //application.get(base64) => file(base64)
}
