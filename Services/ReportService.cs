using LibraryManagementApp.Contexts;
using LibraryManagementApp.Enums;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementApp.Services;

public class ReportService
{
    private readonly LibraryDbContext _context;

    public ReportService()
    {
        _context = new LibraryDbContext();
    }

    public void BorrowedBooksReport()
    {
        var borrowings = _context.Borrowings
            .Include(b => b.Member)
            .Include(b => b.BookCopy)
            .ThenInclude(c => c!.Book)
            .Where(b =>
                b.Status == BorrowingStatus.Borrowed)
            .ToList();

        foreach (var borrowing in borrowings)
        {
            Console.WriteLine(
                $"{borrowing.Member!.FullName} | " +
                $"{borrowing.BookCopy!.Book!.Title} | " +
                $"{borrowing.DueDate.ToShortDateString()}");
        }
    }

    public void OverdueBooksReport()
    {
        var overdueBooks = _context.Borrowings
            .Include(b => b.Member)
            .Include(b => b.BookCopy)
            .ThenInclude(c => c!.Book)
            .Where(b =>
                b.Status == BorrowingStatus.Borrowed &&
                b.DueDate.Date <DateTime.UtcNow.Date)
            .ToList();

        foreach (var borrowing in overdueBooks)
        {
            int days =
                (DateTime.Now.Date -
                 borrowing.DueDate.Date).Days;

            Console.WriteLine(
                $"{borrowing.Member!.FullName} | " +
                $"{borrowing.BookCopy!.Book!.Title} | " +
                $"{days} Days Late");
        }
    }

    public void PendingFineMembersReport()
    {
        var members = _context.Borrowings
            .Include(b => b.Member)
            .Where(b =>
                b.FineAmount > 0 &&
                !b.IsFinePaid)
            .Select(b => b.Member)
            .Distinct()
            .ToList();

        foreach (var member in members)
        {
            Console.WriteLine(
                $"{member!.MemberId} | " +
                $"{member.FullName}");
        }
    }

    public void MostBorrowedBooksReport()
    {
        var books = _context.Borrowings
            .Include(b => b.BookCopy)
            .ThenInclude(c => c!.Book)
            .AsEnumerable()
            .GroupBy(b => b.BookCopy!.Book!.Title)
            .Select(g => new
            {
                Title = g.Key,
                Count = g.Count()
            })
            .OrderByDescending(x => x.Count)
            .Take(5)
            .ToList();

        foreach (var book in books)
        {
            Console.WriteLine(
                $"{book.Title} | Borrowed {book.Count} Times");
        }
    }

    public void AvailableBooksByCategory()
    {
        var books = _context.BookCopies
            .Include(c => c.Book)
            .ThenInclude(b => b!.BookCategory)
            .Where(c =>
                c.Status == BookCopyStatus.Available)
            .ToList();

        var groupedBooks = books
            .GroupBy(c =>
                c.Book!.BookCategory!.CategoryName);

        foreach (var group in groupedBooks)
        {
            Console.WriteLine(
                $"Category : {group.Key}");

            foreach (var copy in group)
            {
                Console.WriteLine(
                    $"{copy.Book!.Title} | " +
                    $"{copy.CopyCode}");
            }

            Console.WriteLine("------------------");
        }
    }

    public void MemberBorrowHistory()
    {
        Console.Write("Enter Member Id : ");

        int memberId =
            Convert.ToInt32(Console.ReadLine());

        var history = _context.Borrowings
            .Include(b => b.BookCopy)
            .ThenInclude(c => c!.Book)
            .Where(b => b.MemberId == memberId)
            .ToList();

        foreach (var borrowing in history)
        {
            Console.WriteLine(
                $"{borrowing.BookCopy!.Book!.Title} | " +
                $"{borrowing.Status} | " +
                $"{borrowing.BorrowDate.ToShortDateString()}");
        }
    }
}