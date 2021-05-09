
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;


namespace _netmvc.Controllers
{
    public class BookController : Controller
    {
        public IActionResult Upload()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Upload(BookCreateViewModel model)
        {
            string name = model.name;
            IFormFile file = model.cover;
            return View(model);
        }


    }
}
