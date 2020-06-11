using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FoundryLanding.Models;
using FoundryLanding.Models.Data;
using FoundryLanding.Models.Home;
using Microsoft.AspNetCore.Http;

namespace FoundryLanding.Controllers
{
    public class HomeController : Controller
    {
        // DATASET
        public static List<User> Users = new List<User>
        {
            new User("eltariel", "0540", "ellie@eltariel.com", "252040036420288512"),
            new User("Maimakterion", "5496", "jadis.ann.smith@gmail.com", "251293654105325568"),
        };
        
        public static List<World> Games = new List<World>
        {
            new World(Users, "Dev", "https://dev.foundry.eltariel.com", path: "/path/to/world"),
            new World(Users, "Ghosts of Shouganai", "https://ghosts-of-shouganai.foundry.eltariel.com", path: "/path/to/world"),
            new World(Users, "Saltmash", "https://saltmash.foundry.eltariel.com", "/path/to/world"),
            new World(Users, "Shadouganai", "https://shadouganai.foundry.eltariel.com", "/path/to/world"),
        };
        
        public static List<Player> Players = new List<Player>
        {
            // NOTES: Read these from <world-path>/data/users.db - they're in NeDB (JSON object per line).
            // Dev
            new Player(Users[0], Games[0], "Name", "xzcvl;dga", "GM"),
            
            // Ghosts of Shouganai
            new Player(Users[0], Games[1], "Thryth", "NTY1NTVhYWI0ZmM5", ""),
            new Player(Users[0], Games[1], "Ann", "NGQ2ZDA1YjdhYTYy", ""),
            new Player(Users[0], Games[1], "Elliekin", "N2U2NDhiZTU5ZThk", ""),
            new Player(Users[0], Games[1], "Dawn", "NmFlMzhlMWZmZTAz", ""),
            new Player(Users[0], Games[1], "Branwen", "MjdjMGQ5MTUyNmI4", ""),
            new Player(Users[0], Games[1], "Wage", "Yjk3YWZlOTI2NDNk", ""),
            

            // Saltmash
            new Player(Users[0], Games[2], "Coral", "1oLDqAEWxXCFYg6M", "PLAYER"),
            new Player(Users[0], Games[2], "Other GM", "UJciiUScKNC2Vs46", "OGM"),
            new Player(Users[0], Games[2], "Gamemaster", "jL80ypXqNSXN0FCR", "GM"),
            new Player(Users[0], Games[2], "Styrm", "jhtc7EAWs25790YB", "PLAYER"),
            new Player(Users[0], Games[2], "ASS", "uGL2iEbfLM4gi0Cv", "ASS"),
            
            // Shadouganai
            new Player(Users[0], Games[3], "Gamemaster", "UhrYXHlql2AhabZT", ""), 
            new Player(Users[0], Games[3], "GM2", "SkDOLsjNX956Sdz3", ""), 
            new Player(Users[0], Games[3], "GM3", "vUMfM4Af2j0Ege6C", ""),
        };
        // END DATASET
        
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            
            // LINK DATASET
            foreach (var player in Players)
            {
                var game = player.World;
                var user = player.User;

                if (!game.Players.Contains(player))
                {
                    game.Players.Add(player);
                }
                if (!game.Users.Contains(user))
                {
                    game.Users.Add(user);
                }

                if (!user.Players.Contains(player))
                {
                    user.Players.Add(player);
                }
                if (!user.Worlds.Contains(game))
                {
                    user.Worlds.Add(game);
                }
            }
            // END LINK DATASET
        }

        public IActionResult Index()
        {
            foreach (var (header, values) in HttpContext.Request.Headers)
            {
                _logger.LogInformation($"Header [{header}] = {string.Join(", ", values)}");
            }

            var headerUser = Models.Data.User.MakeFromHeaders(HttpContext.Request.Headers);
            var user = Users.FirstOrDefault(u => u.Id == (headerUser?.Id ?? "")) ?? Users[0];
            
            return View(new IndexViewModel(user));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Authenticate([FromQuery] string playerId)
        {
            var player = Players.FirstOrDefault(p => p.PlayerHash == playerId);
            if (player == null)
            {
                throw new ArgumentException("Can't find player");
            }
            
            return View(player);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}
