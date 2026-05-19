using LibraryManagementApp.Enums;

namespace LibraryManagementApp.Models;

public class Borrowing
{
    public int BorrowingId { get; set; }


    // Foreign Keys
    public int MemberId { get; set; }

    public int BookCopyId { get; set; }


    public DateTime BorrowDate { get; set; }
        = DateTime.UtcNow;

    public DateTime DueDate { get; set; }

    public DateTime? ReturnDate { get; set; }

    public BorrowingStatus Status { get; set; }
        = BorrowingStatus.Borrowed;

    public decimal FineAmount { get; set; } = 0;

    public bool IsFinePaid { get; set; } = false;


    // Navigation Property
    public Member? Member { get; set; }

    public BookCopy? BookCopy { get; set; }

    public ICollection<FinePayment> FinePayments { get; set; }
        = new List<FinePayment>();
}