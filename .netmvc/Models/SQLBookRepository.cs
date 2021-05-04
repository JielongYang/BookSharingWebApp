
using _netmvc.Models;

public class SQLBookRepository  {

    private readonly BookContext _context;
    public SQLBookRepository(BookContext context) {
        _context = context;
    }
}