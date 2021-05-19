using _netmvc.Models;
using System.Collections.Generic;

public interface IBookRepository {
    Book GetBook(string id);
    IEnumerable<Book> GetAllBooks();

    Book Insert(Book book);


}