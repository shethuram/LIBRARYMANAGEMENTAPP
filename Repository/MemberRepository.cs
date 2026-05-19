using LibraryManagementApp.Contexts;
using LibraryManagementApp.Interfaces.RepositoryInterfaces;
using LibraryManagementApp.Models;

namespace LibraryManagementApp.Repository;

public class MemberRepository : IMemberRepository
{
    private readonly LibraryDbContext _context;

    public MemberRepository()
    {
        _context = new LibraryDbContext();
    }

    public void AddMember(Member member)
    {
        _context.Members.Add(member);
        _context.SaveChanges();
    }

    public List<Member> GetAllMembers()
    {
        return _context.Members.ToList();
    }

    public Member? GetMemberById(int memberId)
    {
        return _context.Members.FirstOrDefault(
            m => m.MemberId == memberId);
    }

    public Member? GetMemberByEmail(string email)
    {
        return _context.Members.FirstOrDefault(
            m => m.Email == email);
    }

    public Member? GetMemberByPhone(string phone)
    {
        return _context.Members.FirstOrDefault(
            m => m.Phone == phone);
    }

    public void UpdateMember(Member member)
    {
        _context.Members.Update(member);
        _context.SaveChanges();
    }
}