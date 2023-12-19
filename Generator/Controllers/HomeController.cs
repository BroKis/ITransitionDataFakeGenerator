using Generator.Bogus;
using Generator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Diagnostics;

namespace Generator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private List<Data> query;
        

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult PartialTable(int itemsCount = 1,int page = 1, string locale ="ru", float errors = 0,int seed = 0)
        { 
            DataFaker faker = new DataFaker(seed + page, locale, itemsCount, errors);
            if (page == 1)
                query = faker.Get(20);
            if(page > 1) 
                query = faker.Get(10);
            faker.ErrorsGenerator(ref query);
            if (query.Count() == 0) return StatusCode(204);// 204 := "No Content"
            return PartialView(query);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
