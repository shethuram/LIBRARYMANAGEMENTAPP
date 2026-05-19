namespace LibraryManagementApp.Exceptions;

public class BorrowLimitExceededException : Exception
{
    public BorrowLimitExceededException()
        : base("Borrowing limit reached")
    {

    }
}