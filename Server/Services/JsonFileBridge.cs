using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Server.Services
{
    public class JsonFileBridge<Entity> : IFileBridge<Entity> where Entity : Models.IEntity
    {
        public JsonFileBridge(string path)
            :base(path)
        {
            using (var stream = new StreamReader(new FileStream(Path, FileMode.OpenOrCreate)))
            {
                if (stream.BaseStream.Length > 0) Entities = JsonSerializer.Deserialize<List<Entity>>(stream.ReadToEnd());
                else Entities = new List<Entity>();
            }
        }

        public override void Save()
        {
            string str = JsonSerializer.Serialize<List<Entity>>(Entities);
            using (var stream = new StreamWriter(new FileStream(Path, FileMode.Create)))
            {
                stream.WriteLine(str);
                stream.Flush();
            }
        }
    }
}
