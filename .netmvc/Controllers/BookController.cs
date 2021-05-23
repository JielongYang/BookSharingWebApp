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
        private readonly BookContext _context;


        public BookController(IBookRepository bookRepository,IWebHostEnvironment webHostEnvironment,BookContext context)
        {
            _bookRepository = new SQLBookRepository();
            _webHostEnvironment =  webHostEnvironment;
            _context = context;
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
        public ViewResult GetAll() {
            IEnumerable<Book> list = _context.Books.ToList();

            

            return View(list);  
        }

        public ViewResult Details() {
            Book model = _bookRepository.GetBook("1");
            ViewBag.Book = model;
            return View();
        }

        [HttpGet("upload")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost("upload")]
        public IActionResult Create(BookCreateViewModel model) {

            if(ModelState.IsValid) {
                string filePath = null;
                string imgPath = null;
                if(model.cover != null && model.entity != null) {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath,"images");
                    // uniqueFileName = Guid.NewGuid().ToString() + "_" + model.cover.FileName;
                    imgPath = Path.Combine(uploadsFolder, model.cover.FileName);
                    filePath = Path.Combine(uploadsFolder, model.entity.FileName);
                    
                    model.cover.CopyTo(new FileStream(imgPath,FileMode.Create));
                    model.entity.CopyTo(new FileStream(filePath,FileMode.Create));
                }
                Book book = new Book() {
                    id = Guid.NewGuid().ToString(),
                    name = model.name,
                    cover = model.cover.FileName,
                    entity = model.entity.FileName
                };
                _context.Books.Add(book);
                _context.SaveChanges();
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
