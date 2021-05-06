using _netmvc.Models;
using System.Collections.Generic;

public interface IBookRepository {
    Book GetBook(int id);
    IEnumerable<Book> GetAllBooks();


}