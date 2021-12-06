using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models
{
    public class InformationCard : IEntity
    {
        private object _locker;
        private string _name;
        public string Name
        {
            get
            {
                lock (_locker) { return _name; }
            }
            set
            {
                lock (_locker) { _name = value; }
            }
        }
        private byte[] _img;
        public byte[] Img
        {
            get
            {
                lock (_locker) { return _img; }
            }
            set
            {
                lock (_locker) { _img = value; }
            }
        }
        public InformationCard()
        {
            _locker = new object();
        }

        public object Clone()
        {
            return new InformationCard() { Name = _name, Img = _img };
        }
    }
}
