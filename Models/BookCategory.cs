namespace LibraryManagementApp.Models;

public class BookCategory
{
    public int BookCategoryId { get; set; }

    public string CategoryName { get; set; } = string.Empty;


    // Navigation Property
    public ICollection<Book> Books { get; set; }
        = new List<Book>();
}