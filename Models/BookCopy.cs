using LibraryManagementApp.Enums;

namespace LibraryManagementApp.Models;

public class BookCopy
{
    public int BookCopyId { get; set; }

    public string CopyCode { get; set; } = string.Empty;

    public BookCopyStatus Status { get; set; }
        = BookCopyStatus.Available;


    // Foreign Key
    public int BookId { get; set; }


    // Navigation Property
    public Book? Book { get; set; }

    public ICollection<Borrowing> Borrowings { get; set; }
        = new List<Borrowing>();
}