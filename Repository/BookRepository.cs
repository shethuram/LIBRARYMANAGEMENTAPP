using LibraryManagementApp.Contexts;
using LibraryManagementApp.Enums;
using LibraryManagementApp.Interfaces.RepositoryInterfaces;
using LibraryManagementApp.Models;
using Microsoft.EntityFrameworkCore;


namespace LibraryManagementApp.Repository;

public class BookRepository : IBookRepository
{
    private readonly LibraryDbContext _context;

    public BookRepository()
    {
        _context = new LibraryDbContext();
    }

    public void AddBook(Book book)
    {
        _context.Books.Add(book);
        _context.SaveChanges();
    }

    public void AddBookCopy(BookCopy copy)
    {
        _context.BookCopies.Add(copy);
        _context.SaveChanges();
    }

    public List<Book> GetAllBooks()
    {
        return _context.Books
            .Include(b => b.BookCategory)
            .ToList();
    }

    public List<Book> SearchBooks(string keyword)
    {
        keyword = keyword.ToLower();

        return _context.Books
            .Include(b => b.BookCategory)
            .Where(b =>
                b.Title.ToLower().Contains(keyword) ||
                b.Author.ToLower().Contains(keyword) ||
                b.BookCategory!.CategoryName.ToLower().Contains(keyword))
            .ToList();
    }

    public Book? GetBookById(int bookId)
    {
        return _context.Books.FirstOrDefault(
            b => b.BookId == bookId);
    }

    public BookCopy? GetAvailableCopy(int bookId)
    {
        return _context.BookCopies.FirstOrDefault(
            c => c.BookId == bookId &&
                 c.Status == BookCopyStatus.Available);
    }

    public BookCopy? GetBookCopyById(int copyId)
    {
        return _context.BookCopies
            .Include(c => c.Book)
            .FirstOrDefault(c => c.BookCopyId == copyId);
    }

    public void UpdateBookCopy(BookCopy copy)
    {
        _context.BookCopies.Update(copy);
        _context.SaveChanges();
    }


  
}