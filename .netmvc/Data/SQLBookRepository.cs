
using _netmvc.Models;
using System.Collections.Generic;
using System.Linq;
public class SQLBookRepository : IBookRepository  {

    private readonly BookContext _context;
    private List<Book> _bookList;

      public IEnumerable<Book> GetAllBooks() {

            return _context.Books.ToList();
    }
    public Book GetBook(string id) {
        return _bookList.FirstOrDefault(x => x.id == id);
    }

    public Book Insert(Book book) {
        book.id = _bookList.Max(b => b.id) + 1;
        _bookList.Add(book);
        return book;
    }

}