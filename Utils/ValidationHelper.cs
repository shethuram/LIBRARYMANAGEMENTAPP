using System.Text.RegularExpressions;

namespace LibraryManagementApp.Utils;

public static class ValidationHelper
{
    public static bool IsValidName(string name)
    {
        return !string.IsNullOrWhiteSpace(name);
    }

    public static bool IsValidEmail(string email)
    {
        string pattern =
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

        return Regex.IsMatch(email, pattern);
    }

    public static bool IsValidPhone(string phone)
    {
        string pattern =
            @"^[0-9]{10}$";

        return Regex.IsMatch(phone, pattern);
    }
}