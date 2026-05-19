namespace LibraryManagementApp.Models;

public class Book
{
    public int BookId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Author { get; set; } = string.Empty;


    // Foreign Key
    public int BookCategoryId { get; set; }


    // Navigation Property
    public BookCategory? BookCategory { get; set; }

    public ICollection<BookCopy> BookCopies { get; set; }
        = new List<BookCopy>();
}