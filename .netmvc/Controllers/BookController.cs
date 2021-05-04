
using Microsoft.AspNetCore.Mvc;


namespace _netmvc.Controllers
{
    public class BookController : Controller
    {
        // private readonly ;

        // public BookController()
        // {
            
        // }
        public string index()
        {
            return "Book";
        }
        public string list(){
            return "list";
        }


    }
}
