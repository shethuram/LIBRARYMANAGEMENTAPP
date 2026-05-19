using LibraryManagementApp.Contexts;
using LibraryManagementApp.Enums;
using LibraryManagementApp.Interfaces.RepositoryInterfaces;
using LibraryManagementApp.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;


namespace LibraryManagementApp.Repository;

public class BorrowingRepository : IBorrowingRepository
{
    private readonly LibraryDbContext _context;

    public BorrowingRepository()
    {
        _context = new LibraryDbContext();
    }

    public void AddBorrowing(Borrowing borrowing)
    {
        _context.Borrowings.Add(borrowing);
        _context.SaveChanges();
    }

    public Borrowing? GetBorrowingById(int borrowingId)
    {
        return _context.Borrowings
            .Include(b => b.Member)
            .Include(b => b.BookCopy)
            .ThenInclude(c => c!.Book)
            .FirstOrDefault(b => b.BorrowingId == borrowingId);
    }

    public List<Borrowing> GetActiveBorrowingsByMember(int memberId)
    {
        return _context.Borrowings
            .Include(b => b.BookCopy)
            .ThenInclude(c => c!.Book)
            .Where(b =>
                b.MemberId == memberId &&
                b.Status == BorrowingStatus.Borrowed)
            .ToList();
    }

    public bool HasActiveBorrowing(int memberId, int bookId)
    {
        return _context.Borrowings
            .Include(b => b.BookCopy)
            .Any(b =>
                b.MemberId == memberId &&
                b.BookCopy!.BookId == bookId &&
                b.Status == BorrowingStatus.Borrowed);
    }

    public decimal GetPendingFine(int memberId)
    {
        return _context.Borrowings
            .Where(b =>
                b.MemberId == memberId &&
                b.FineAmount > 0 &&
                !b.IsFinePaid)
            .Sum(b => b.FineAmount);
    }

    public void UpdateBorrowing(Borrowing borrowing)
    {
        _context.Borrowings.Update(borrowing);
        _context.SaveChanges();
    }


      public decimal GetFineUsingFunction(int memberId)
    {
        var connection =
            _context.Database.GetDbConnection();

        connection.Open();

        using var command =
            connection.CreateCommand();

        command.CommandText =
            "SELECT calculate_member_fine(@memberId)";

        var parameter =
            new NpgsqlParameter(
                "@memberId",
                memberId);

        command.Parameters.Add(parameter);

        decimal totalFine =
            Convert.ToDecimal(command.ExecuteScalar());

        connection.Close();

        return totalFine;
    }
}