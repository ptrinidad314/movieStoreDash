using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using movieStoreDash.Models;
using movieStoreDash.Services;

namespace movieStoreDash.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly IMovieStoreDashRepository _movieStoreDashRepository;

        public HomeController(IMovieStoreDashRepository movieStoreDashRepository)//ILogger<HomeController> logger)
        {
            //_logger = logger;
            _movieStoreDashRepository = movieStoreDashRepository;
        }

        public IActionResult Index()
        {
            var model = _movieStoreDashRepository.GetHomeIndexDTO();

            return View(model);
        }

        [HttpPost]
        public ActionResult GetActors(int filmId) 
        {
            var model = _movieStoreDashRepository.GetFilmActors(filmId);

            return Ok(model);
        }

        [HttpPost]
        public ActionResult GetActorInfo(int actorId) 
        {
            var model = _movieStoreDashRepository.GetActorInfo(actorId);

            return Ok(model);
        }

        [HttpPost]
        public void UpdateActorInfo(string bio, string firstName, string lastName, string actorId, string socialMediaURL) 
        {
            int iActorId = int.Parse(actorId);

            _movieStoreDashRepository.UpdateActorInfo(iActorId, bio, firstName, lastName, socialMediaURL);          
        }


        public void RunTest(int testNum) 
        {
            _movieStoreDashRepository.RunTest();
        }





        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
