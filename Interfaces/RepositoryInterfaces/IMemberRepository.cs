using LibraryManagementApp.Models;

namespace LibraryManagementApp.Interfaces.RepositoryInterfaces;

public interface IMemberRepository
{
    void AddMember(Member member);

    List<Member> GetAllMembers();

    Member? GetMemberById(int memberId);

    Member? GetMemberByEmail(string email);

    Member? GetMemberByPhone(string phone);

    void UpdateMember(Member member);
}