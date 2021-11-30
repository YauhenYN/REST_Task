using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Services
{
    public abstract class IFileBridge<Context> : IDisposable where Context : Models.IEntity
    {
        public string Path { get; protected set; }
        public List<Context> Contexts { get; protected set; }
        public event Action OnDispose;

        public IFileBridge(string path)
        {
            Path = path;
        }
        public void Dispose()
        {
            OnDispose?.Invoke();
            Save();
        }
        public abstract void Save();

    }
}
