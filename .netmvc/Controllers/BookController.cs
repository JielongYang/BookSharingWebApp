using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using _netmvc.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.StaticFiles;

namespace _netmvc.Controllers
{
    public class BookController : Controller
    {
         private readonly IBookRepository _bookRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public BookController(IBookRepository bookRepository,IWebHostEnvironment webHostEnvironment)
        {
            _bookRepository = new MockBookRepository();
            _webHostEnvironment =  webHostEnvironment;
        }

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
         [HttpGet("/")]
        public IEnumerable<Book> GetAll() {
            IEnumerable<Book> list = _bookRepository.GetAllBooks();

            return list;  
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
