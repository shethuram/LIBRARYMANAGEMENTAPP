using LibraryManagementApp.Contexts;
using LibraryManagementApp.Models;
using LibraryManagementApp.Repository;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementApp.Services;

public class FineService
{
    private readonly LibraryDbContext _context;

    private readonly BorrowingRepository _borrowingRepository;

    public FineService()
    {
        _context = new LibraryDbContext();

        _borrowingRepository =
            new BorrowingRepository();
    }

    public void ViewPendingFines()
    {
        Console.Write("Enter Member Id : ");

        int memberId =
            Convert.ToInt32(Console.ReadLine());

        var borrowings = _context.Borrowings
            .Include(b => b.BookCopy)
            .ThenInclude(c => c!.Book)
            .Where(b =>
                b.MemberId == memberId &&
                b.FineAmount > 0 &&
                !b.IsFinePaid)
            .ToList();

        if (!borrowings.Any())
        {
            Console.WriteLine("No Pending Fines");
            return;
        }

        decimal totalFine = 0;

        foreach (var borrowing in borrowings)
        {
            Console.WriteLine(
                $"Borrowing Id : {borrowing.BorrowingId}");

            Console.WriteLine(
                $"Book : {borrowing.BookCopy!.Book!.Title}");

            Console.WriteLine(
                $"Fine : ₹{borrowing.FineAmount}");

            Console.WriteLine("-------------------");

            totalFine += borrowing.FineAmount;
        }

        Console.WriteLine(
            $"Total Pending Fine : ₹{totalFine}");
    }

    public void PayFine()
    {
        Console.Write("Enter Borrowing Id : ");

        int borrowingId =
            Convert.ToInt32(Console.ReadLine());

        Borrowing? borrowing =
            _context.Borrowings
            .FirstOrDefault(
                b => b.BorrowingId == borrowingId);

        if (borrowing == null)
        {
            Console.WriteLine("Borrowing Not Found");
            return;
        }

        if (borrowing.FineAmount <= 0)
        {
            Console.WriteLine("No Fine Available");
            return;
        }

        if (borrowing.IsFinePaid)
        {
            Console.WriteLine("Fine Already Paid");
            return;
        }

        FinePayment payment = new()
        {
            BorrowingId = borrowing.BorrowingId,
            AmountPaid = borrowing.FineAmount,
            PaidDate =DateTime.UtcNow
        };

        borrowing.IsFinePaid = true;

        _context.FinePayments.Add(payment);

        _context.Borrowings.Update(borrowing);

        _context.SaveChanges();

        Console.WriteLine("Fine Paid Successfully");
    }

    public void FineHistory()
    {
        Console.Write("Enter Member Id : ");

        int memberId =
            Convert.ToInt32(Console.ReadLine());

        var payments = _context.FinePayments
            .Include(f => f.Borrowing)
            .ThenInclude(b => b!.BookCopy)
            .ThenInclude(c => c!.Book)
            .Where(f =>
                f.Borrowing!.MemberId == memberId)
            .ToList();

        if (!payments.Any())
        {
            Console.WriteLine("No Fine History");
            return;
        }

        foreach (var payment in payments)
        {
            Console.WriteLine(
                $"Book : " +
                $"{payment.Borrowing!.BookCopy!.Book!.Title}");

            Console.WriteLine(
                $"Paid Amount : ₹{payment.AmountPaid}");

            Console.WriteLine(
                $"Paid Date : {payment.PaidDate}");

            Console.WriteLine("--------------------");
        }
    }

    public void GetFineUsingFunction()
    {
        Console.Write("Enter Member Id : ");

        int memberId =
            Convert.ToInt32(Console.ReadLine());

        decimal totalFine =
            _borrowingRepository
            .GetFineUsingFunction(memberId);

        Console.WriteLine(
            $"Total Pending Fine : ₹{totalFine}");
    }
}