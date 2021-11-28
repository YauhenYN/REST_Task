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
    //file(non-encoded) => application.set(base64:IFileBridge)
    //user(base64) => application.set(empty)
    //user(base64) <= application.get(empty)
    //application.get(base64) => file(non-encoded)
}
