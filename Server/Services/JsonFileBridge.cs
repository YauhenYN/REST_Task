using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Server.Services
{
    public class JsonFileBridge<Context> : IFileBridge<Context> where Context : Models.IEntity
    {
        public JsonFileBridge(string path)
            :base(path)
        {
            using (var stream = new StreamReader(new FileStream(Path, FileMode.OpenOrCreate)))
            {
                if (stream.BaseStream.Length > 0) Contexts = JsonSerializer.Deserialize<List<Context>>(stream.ReadToEnd());
                else Contexts = new List<Context>();
            }
        }

        public override void Save()
        {
            string str = JsonSerializer.Serialize<List<Context>>(Contexts);
            using (var stream = new StreamWriter(new FileStream(Path, FileMode.Create)))
            {
                stream.WriteLine(str);
                stream.Flush();
            }
        }
    }
}
