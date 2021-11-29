using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models
{
    public class InformationCard
    {
        public string Name { get; set; }
        public string Img { get; set; }
        public bool Validate()
        {
            if (Name != null && Name != null) return true;
            return false;
        }
    }
    //img
    //file(base64) => application.set(base64:IFileBridge)
    //user(base64) => application.set(base64)
    //user(base64) <= application.get(base64)
    //application.get(base64) => file(base64)
}
