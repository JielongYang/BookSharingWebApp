using Microsoft.AspNetCore.Http;

public class BookCreateViewModel {
    public string name{get;set;}
    public IFormFile cover{get;set;}
}