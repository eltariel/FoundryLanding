using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using WorldReader;
using JsonConverter = Newtonsoft.Json.JsonConverter;

namespace WorldReaderTester
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Needs one argument for host data path");
            }
            
            var host = new Host(args[0]).Load();
            Console.WriteLine(JsonConvert.SerializeObject(host, Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                    PreserveReferencesHandling = PreserveReferencesHandling.All,
                }));
        }
    }
}