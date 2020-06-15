using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Foundry.Landing.Models;
using Foundry.Landing.Models.Data;
using Foundry.WorldReader;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Foundry.Landing.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly List<Host> hosts;
        private DiscordUser user;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            var dataPath = configuration.GetSection("Foundry")["DataPath"];
            this.logger = logger;

            hosts = ReadHostData(dataPath);
        }

        public IActionResult Index()
        {
            foreach (var (header, values) in HttpContext.Request.Headers)
            {
                logger.LogInformation($"Header [{header}] = {string.Join(", ", values)}");
            }

            user = PopulateDiscordUser();
            return View(user);
        }

        public IActionResult Authenticate([FromQuery] string playerId)
        {
            user = PopulateDiscordUser();
            var foundryUser = user.FoundryUsers.FirstOrDefault(u => u.Id == playerId);
            if (foundryUser == null)
            {
                throw new ArgumentException("Can't find player");
            }
            
            return View(foundryUser);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

        private DiscordUser PopulateDiscordUser()
        {
            var discordUser = DiscordUser.MakeFromHeaders(HttpContext.Request.Headers);

            var mine = hosts
                .SelectMany(h => h.Worlds)
                .SelectMany(w => w.Users)
                .Where(u =>
                    u.DiscordUser == discordUser.FullName ||
                    u.World.Users.Any(wu=> 
                        wu.Role == UserRole.Gamemaster &&
                        wu.DiscordUser == discordUser.FullName) ||
                    u.World.Owners.Contains(discordUser.FullName));

            discordUser.FoundryUsers.AddRange(mine);
            return discordUser;
        }

        private static List<Host> ReadHostData(string dataPath)
        {
            var hosts = new List<Host>();
            var hostDirs = Directory.EnumerateDirectories(dataPath);
            
            foreach (var hostDir in hostDirs)
            {
                var hostPath = Path.Combine(dataPath, hostDir);
                var host = new Host(hostPath).Load();
                hosts.Add(host);
            }

            return hosts;
        }
    }
}
