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
            new World(Users, "Dev", new Uri("https://dev.foundry.eltariel.com"), path: "/path/to/world"),
            new World(Users, "Saltmash", new Uri("https://saltmash.foundry.eltariel.com"), "/path/to/world"),
            new World(Users, "Shadouganai", new Uri("https://shadouganai.foundry.eltariel.com"), "/path/to/world"),
        };
        
        public static List<Player> Players = new List<Player>
        {
            // NOTES: Read these from <world-path>/data/users.db - they're in NeDB (JSON object per line).
            // Dev
            new Player(Users[0], Games[0], "Name", "xzcvl;dga", "GM"),

            // Saltmash
            new Player(Users[0], Games[1], "Coral", "1oLDqAEWxXCFYg6M", "PLAYER"),
            new Player(Users[0], Games[1], "Other GM", "UJciiUScKNC2Vs46", "OGM"),
            new Player(Users[0], Games[1], "Gamemaster", "jL80ypXqNSXN0FCR", "GM"),
            new Player(Users[0], Games[1], "Styrm", "jhtc7EAWs25790YB", "PLAYER"),
            new Player(Users[0], Games[1], "ASS", "uGL2iEbfLM4gi0Cv", "ASS"),
            
            // Shadouganai
            new Player(Users[0], Games[2], "Gamemaster", "UhrYXHlql2AhabZT", ""), 
            new Player(Users[0], Games[2], "GM2", "SkDOLsjNX956Sdz3", ""), 
            new Player(Users[0], Games[2], "GM3", "vUMfM4Af2j0Ege6C", ""),
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
            var user = Users.FirstOrDefault(u => u.Id == (headerUser?.Id ?? ""));
            
            return View(new IndexViewModel(user));
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
    }
}