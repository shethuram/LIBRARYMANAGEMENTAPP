using LibraryManagementApp.Contexts;
using LibraryManagementApp.Enums;
using LibraryManagementApp.Models;
using LibraryManagementApp.Repository;

namespace LibraryManagementApp.Services;

public class BookService
{
    private readonly BookRepository _bookRepository;

    private readonly LibraryDbContext _context;

    public BookService()
    {
        _bookRepository = new BookRepository();

        _context = new LibraryDbContext();
    }

    public void AddCategory()
    {
        Console.Write("Enter Category Name : ");

        string categoryName = Console.ReadLine()!;

        BookCategory category = new()
        {
            CategoryName = categoryName
        };

        _context.BookCategories.Add(category);

        _context.SaveChanges();

        Console.WriteLine("Category Added");
    }

    public void AddBook()
    {
        var categories =
            _context.BookCategories.ToList();

        if (!categories.Any())
        {
            Console.WriteLine("No Categories Found");
            return;
        }

        foreach (var category in categories)
        {
            Console.WriteLine(
                $"{category.BookCategoryId} - " +
                $"{category.CategoryName}");
        }

        Console.Write("Enter Title : ");
        string title = Console.ReadLine()!;

        Console.Write("Enter Author : ");
        string author = Console.ReadLine()!;

        Console.Write("Enter Category Id : ");

        int categoryId =
            Convert.ToInt32(Console.ReadLine());

        Book book = new()
        {
            Title = title,
            Author = author,
            BookCategoryId = categoryId
        };

        _bookRepository.AddBook(book);

        Console.WriteLine("Book Added");
    }

    public void AddBookCopy()
    {
        var books = _bookRepository.GetAllBooks();

        foreach (var book in books)
        {
            Console.WriteLine(
                $"{book.BookId} - {book.Title}");
        }

        Console.Write("Enter Book Id : ");

        int bookId =
            Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter Copy Code : ");

        string copyCode =
            Console.ReadLine()!;

        BookCopy copy = new()
        {
            BookId = bookId,
            CopyCode = copyCode,
            Status = BookCopyStatus.Available
        };

        _bookRepository.AddBookCopy(copy);

        Console.WriteLine("Book Copy Added");
    }

    public void ViewBooks()
    {
        var books = _bookRepository.GetAllBooks();

        foreach (var book in books)
        {
            Console.WriteLine(
                $"{book.BookId} | " +
                $"{book.Title} | " +
                $"{book.Author} | " +
                $"{book.BookCategory!.CategoryName}");
        }
    }

    public void SearchBooks()
    {
        Console.Write("Enter Keyword : ");

        string keyword = Console.ReadLine()!;

        var books =
            _bookRepository.SearchBooks(keyword);

        foreach (var book in books)
        {
            Console.WriteLine(
                $"{book.BookId} | " +
                $"{book.Title} | " +
                $"{book.Author}");
        }
    }

    public void MarkCopyDamaged()
    {
        Console.Write("Enter Copy Id : ");

        int copyId =
            Convert.ToInt32(Console.ReadLine());

        BookCopy? copy =
            _bookRepository.GetBookCopyById(copyId);

        if (copy == null)
        {
            Console.WriteLine("Copy Not Found");
            return;
        }

        copy.Status = BookCopyStatus.Damaged;

        _bookRepository.UpdateBookCopy(copy);

        Console.WriteLine("Copy Marked Damaged");
    }
}