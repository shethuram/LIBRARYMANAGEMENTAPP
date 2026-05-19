using LibraryManagementApp.Enums;

namespace LibraryManagementApp.Models;

public class Member
{
    public int MemberId { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public MembershipType MembershipType { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } =DateTime.UtcNow;


    // Navigation Property
    public ICollection<Borrowing> Borrowings { get; set; }
        = new List<Borrowing>();
}