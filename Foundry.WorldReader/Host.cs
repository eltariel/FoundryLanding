using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Foundry.WorldReader
{
    public class Host
    {
        public Host(string dataPath)
        {
            DataPath = dataPath;
        }

        public string DataPath { get; }
        public string Url { get; private set; }

        public World DefaultWorld { get; private set; }
        public List<World> Worlds { get; } = new List<World>();

        public Host Load()
        {
            var options = JObject.Parse(File.ReadAllText(Path.Combine(DataPath, "Config", "options.json")));
            var defWorld = (string)options["world"];
            Url = (string)options["url"];

            LoadWorlds(defWorld);
            return this;
        }

        private void LoadWorlds(string defWorld)
        {
            DefaultWorld = null;
            var worldPaths = Directory.EnumerateDirectories(Path.Combine(DataPath, "Data", "worlds"));
            foreach (var worldPath in worldPaths)
            {
                var world = new World(this, worldPath).Load();
                Worlds.Add(world);
                if (worldPath == defWorld)
                {
                    DefaultWorld = world;
                }
            }
        }
    }
}