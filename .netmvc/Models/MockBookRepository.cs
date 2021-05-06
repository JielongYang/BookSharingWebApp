using _netmvc.Models;
using System.Collections.Generic;
using System.Linq;

public class MockBookRepository : IBookRepository {
    private readonly Book _book;
    private List<Book> _bookList;

    public MockBookRepository() {
        _bookList = new List<Book>() {
            new Book(){
                id = 1,
                name = "book1",
                cover="cover1",
                entity = "entity1"
                },
            new Book(){
                id = 2,
                name = "book2",
                cover="cover2",
                entity = "entity2"
                }
        };

    }
    public IEnumerable<Book> GetAllBooks() {
            return _bookList;
    }
    public Book GetBook(int id) {
        return _bookList.FirstOrDefault(x => x.id == id);
    }
        
    
    
  
}