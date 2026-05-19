namespace LibraryManagementApp.Exceptions;

public class BookUnavailableException : Exception
{
    public BookUnavailableException()
        : base("Book copy not available")
    {

    }
}