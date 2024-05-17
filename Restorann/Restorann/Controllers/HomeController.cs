using Microsoft.AspNetCore.Mvc;
using Restorann.Business.Services.Abstracts;
using System.Diagnostics;

namespace Restorann.Controllers
{
    public class HomeController : Controller
    {
        private readonly IChefService _chefService;

        public HomeController(IChefService chefService)
        {
            _chefService = chefService;
        }

        public IActionResult Index()
        {
            var chefs = _chefService.GetAllChefs();
            return View(chefs);
        }

    }
}