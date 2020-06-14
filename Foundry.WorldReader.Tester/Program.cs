using System;
using Newtonsoft.Json;

namespace Foundry.WorldReader.Tester
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