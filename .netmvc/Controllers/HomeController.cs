using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using _netmvc.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
namespace _netmvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public HomeController(IBookRepository bookRepository,IWebHostEnvironment webHostEnvironment)
        {
            _bookRepository = new MockBookRepository();
            _webHostEnvironment =  webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
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

        public ViewResult Details() {
            Book model = _bookRepository.GetBook(1);
            ViewBag.Book = model;
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(BookCreateViewModel model) {

            if(ModelState.IsValid) {
                string uniqueFileName = null;
                if(model.cover != null) {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath,"images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.cover.FileName;
                    string filePath = Path.Combine(uploadsFolder,uniqueFileName);
                    model.cover.CopyTo(new FileStream(filePath,FileMode.Create));
                }
                Book book = new Book() {
                    name = model.name,
                    cover = uniqueFileName
                };
                _bookRepository.Insert(book);
                return View(model);
            }
            return View(model);
        }

        public IActionResult Download(){
            return View();
        }

        [HttpGet("Download/{filename}")]
        public FileStream Download(string filename){
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath,"images");
            string filePath = Path.Combine(uploadsFolder,filename);
            FileStream file = new FileStream(filePath,FileMode.Open);
            
            // var stream = System.IO.File.OpenRead(filePath);
            // string fileExt = Path.GetExtension(filename);
            // var provider = new FileExtensionContentTypeProvider();
            // var memi = provider.Mappings[fileExt];

            return file;
            
        }
    }
}
