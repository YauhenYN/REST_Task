using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Services
{
    public abstract class IFileBridge<Context> : IDisposable
    {
        public string Path { get; protected set; }
        public List<Context> Contexts { get; set; }

        public IFileBridge(string path)
        {
            Path = path;
        }
        public abstract void Dispose();

        public abstract void Save();

    }
}
