namespace LibraryManagementApp.Models;

public class FinePayment
{
    public int FinePaymentId { get; set; }

    public decimal AmountPaid { get; set; }

    public DateTime PaidDate { get; set; }
        =DateTime.UtcNow;


    // Foreign Key
    public int BorrowingId { get; set; }


    // Navigation Property
    public Borrowing? Borrowing { get; set; }
}