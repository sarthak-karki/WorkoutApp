using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using WorkoutTracker_v2.Models;

namespace WorkoutTracker_v2.Controllers
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
            var exercises = new List<Exercise>();
            var exercise1 = new Exercise(1, "BenchPress", "Compound", "UpperBody", "Chest", 3, 8);
            var exercise2 = new Exercise(2, "Squat", "Compound", "LowerBody", "Quads", 3, 8);
            var exercise3 = new Exercise(2, "DeadLift", "Compound", "LowerBody", "Hams", 3, 8);
            var exercise4 = new Exercise(2, "Bicep Curls", "Isolation", "UpperBody", "Biceps", 3, 12);
            var exercise5 = new Exercise(2, "CalvesRaises", "Isolation", "LowerBody", "Calves", 3, 15);

            exercises.Add(exercise1);
            exercises.Add(exercise2);
            exercises.Add(exercise3);
            exercises.Add(exercise4);
            exercises.Add(exercise5);

            var model = new Workout(exercises);
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
