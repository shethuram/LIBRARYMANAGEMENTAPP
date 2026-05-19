using LibraryManagementApp.Contexts;
using LibraryManagementApp.Enums;
using LibraryManagementApp.Exceptions;
using LibraryManagementApp.Models;
using LibraryManagementApp.Repository;
using LibraryManagementApp.Utils;

namespace LibraryManagementApp.Services;

public class BorrowingService
{
    private readonly MemberRepository _memberRepository;

    private readonly BookRepository _bookRepository;

    private readonly BorrowingRepository _borrowingRepository;

    private readonly LibraryDbContext _context;

    public BorrowingService()
    {
        _memberRepository = new MemberRepository();

        _bookRepository = new BookRepository();

        _borrowingRepository = new BorrowingRepository();

        _context = new LibraryDbContext();
    }

    public void BorrowBook()
    {
        using var transaction =
            _context.Database.BeginTransaction();

        try
        {
            Console.Write("Enter Member Id : ");

            if (!int.TryParse(
                Console.ReadLine(),
                out int memberId))
            {
                Console.WriteLine(
                    "Invalid Member Id");

                return;
            }

            Member? member =
                _memberRepository
                .GetMemberById(memberId);

            if (member == null)
            {
                Console.WriteLine(
                    "Invalid Member");

                return;
            }

            if (!member.IsActive)
            {
                Console.WriteLine(
                    "Inactive Member");

                return;
            }

            decimal pendingFine =
                _borrowingRepository
                .GetPendingFine(memberId);

            if (pendingFine > 500)
            {
                throw new FineLimitExceededException();
            }

            int activeBorrowCount =
                _borrowingRepository
                .GetActiveBorrowingsByMember(memberId)
                .Count;

            int limit =
                MembershipRules.GetBorrowLimit(
                    member.MembershipType);

            if (activeBorrowCount >= limit)
            {
                throw new BorrowLimitExceededException();
            }

            Console.Write("Enter Book Id : ");

            if (!int.TryParse(
                Console.ReadLine(),
                out int bookId))
            {
                Console.WriteLine(
                    "Invalid Book Id");

                return;
            }

            bool alreadyBorrowed =
                _borrowingRepository
                .HasActiveBorrowing(
                    memberId,
                    bookId);

            if (alreadyBorrowed)
            {
                Console.WriteLine(
                    "Book Already Borrowed");

                return;
            }

            BookCopy? copy =
                _bookRepository
                .GetAvailableCopy(bookId);

            if (copy == null)
            {
                throw new BookUnavailableException();
            }

            int days =
                MembershipRules.GetBorrowDays(
                    member.MembershipType);

            Borrowing borrowing = new()
            {
                MemberId = memberId,

                BookCopyId = copy.BookCopyId,

                BorrowDate = DateTime.UtcNow,

                DueDate =
                    DateTime.UtcNow.AddDays(days),

                Status =
                    BorrowingStatus.Borrowed
            };

            _context.Borrowings.Add(borrowing);

            copy.Status =
                BookCopyStatus.Borrowed;

            _context.BookCopies.Update(copy);

            _context.SaveChanges();

            transaction.Commit();

            Console.WriteLine(
                "Book Borrowed Successfully");
        }
        catch (Exception ex)
        {
            transaction.Rollback();

            Console.WriteLine(ex.Message);
        }
    }

    public void ReturnBook()
    {
        try
        {
            Console.Write(
                "Enter Borrowing Id : ");

            if (!int.TryParse(
                Console.ReadLine(),
                out int borrowingId))
            {
                Console.WriteLine(
                    "Invalid Borrowing Id");

                return;
            }

            Borrowing? borrowing =
                _context.Borrowings
                .FirstOrDefault(
                    b => b.BorrowingId == borrowingId);

            if (borrowing == null)
            {
                Console.WriteLine(
                    "Borrowing Not Found");

                return;
            }

            if (borrowing.Status ==
                BorrowingStatus.Returned)
            {
                Console.WriteLine(
                    "Already Returned");

                return;
            }

            BookCopy? copy =
                _context.BookCopies
                .FirstOrDefault(
                    c => c.BookCopyId ==
                        borrowing.BookCopyId);

            if (copy == null)
            {
                Console.WriteLine(
                    "Book Copy Not Found");

                return;
            }

            borrowing.ReturnDate =
                DateTime.UtcNow;

            int delayedDays =
                (DateTime.UtcNow.Date -
                borrowing.DueDate.Date).Days;

            if (delayedDays > 0)
            {
                borrowing.FineAmount =
                    delayedDays * 10;
            }

            borrowing.Status =
                BorrowingStatus.Returned;

            copy.Status =
                BookCopyStatus.Available;

            _context.SaveChanges();

            Console.WriteLine(
                "Book Returned Successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}