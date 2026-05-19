using LibraryManagementApp.Models;

namespace LibraryManagementApp.Interfaces.RepositoryInterfaces;

public interface IBookRepository
{
    void AddBook(Book book);

    void AddBookCopy(BookCopy copy);

    List<Book> GetAllBooks();

    List<Book> SearchBooks(string keyword);

    Book? GetBookById(int bookId);

    BookCopy? GetAvailableCopy(int bookId);

    BookCopy? GetBookCopyById(int copyId);

    void UpdateBookCopy(BookCopy copy);
}