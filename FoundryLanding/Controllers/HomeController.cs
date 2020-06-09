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

namespace FoundryLanding.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            foreach (var (header, values) in HttpContext.Request.Headers)
            {
                _logger.LogInformation($"Header [{header}] = {string.Join(", ", values)}");
            }

            var userName = HttpContext.Request.Headers["X-Vouch-User"][0];
            var discriminator = HttpContext.Request.Headers["X-Vouch-IdP-Claims-Discriminator"][0];
            var email = HttpContext.Request.Headers["X-Vouch-IdP-Claims-Email"][0];
            var userid = HttpContext.Request.Headers["X-Vouch-IdP-Claims-Id"][0];
            
            var user = new UserModel(userName, discriminator, email, userid);
            
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