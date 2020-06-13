using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Newtonsoft.Json.Linq;

namespace WorldReader
{
    public class Host
    {
        public Host(string dataPath)
        {
            DataPath = dataPath;
        }

        public string DataPath { get; }

        public World DefaultWorld { get; private set; }
        public List<World> Worlds { get; } = new List<World>();

        public Host Load()
        {
            var options = JObject.Parse(File.ReadAllText(Path.Combine(DataPath, "Config", "options.json")));
            var defWorld = (string)options["world"];

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