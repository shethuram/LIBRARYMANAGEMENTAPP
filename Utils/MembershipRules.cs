using LibraryManagementApp.Enums;

namespace LibraryManagementApp.Utils;

public static class MembershipRules
{
    public static int GetBorrowLimit(MembershipType type)
    {
        return type switch
        {
            MembershipType.Basic => 2,
            MembershipType.Student => 3,
            MembershipType.Premium => 5,
            _ => 0
        };
    }

    public static int GetBorrowDays(MembershipType type)
    {
        return type switch
        {
            MembershipType.Basic => 7,
            MembershipType.Student => 10,
            MembershipType.Premium => 15,
            _ => 0
        };
    }
}