namespace LibraryManagementApp.Exceptions;

public class FineLimitExceededException : Exception
{
    public FineLimitExceededException()
        : base("Pending fine exceeds ₹500")
    {

    }
}