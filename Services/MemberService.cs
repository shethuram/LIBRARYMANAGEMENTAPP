using LibraryManagementApp.Enums;
using LibraryManagementApp.Models;
using LibraryManagementApp.Repository;
using LibraryManagementApp.Utils;

namespace LibraryManagementApp.Services;

public class MemberService
{
    private readonly MemberRepository _memberRepository;

    public MemberService()
    {
        _memberRepository = new MemberRepository();
    }

    public void AddMember()
    {
        Console.Write("Enter Name : ");

        string name = Console.ReadLine()!;

        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Invalid Name");
            return;
        }

        Console.Write("Enter Phone : ");

        string phone = Console.ReadLine()!;

        if (!ValidationHelper.IsValidPhone(phone))
        {
            Console.WriteLine(
                "Invalid Phone Number");

            return;
        }

        if (_memberRepository
            .GetMemberByPhone(phone) != null)
        {
            Console.WriteLine(
                "Phone Number Already Exists");

            return;
        }

        Console.Write("Enter Email : ");

        string email = Console.ReadLine()!;

        if (!ValidationHelper.IsValidEmail(email))
        {
            Console.WriteLine(
                "Invalid Email");

            return;
        }

        if (_memberRepository
            .GetMemberByEmail(email) != null)
        {
            Console.WriteLine(
                "Email Already Exists");

            return;
        }

        Console.WriteLine("1. Basic");

        Console.WriteLine("2. Student");

        Console.WriteLine("3. Premium");

        Console.Write("Choose Membership Type : ");

        if (!int.TryParse(
            Console.ReadLine(),
            out int choice))
        {
            Console.WriteLine(
                "Invalid Membership Choice");

            return;
        }

        if (choice < 1 || choice > 3)
        {
            Console.WriteLine(
                "Invalid Membership Type");

            return;
        }

        MembershipType membershipType =
            (MembershipType)choice;

        Member member = new()
        {
            FullName = name,
            Phone = phone,
            Email = email,
            MembershipType = membershipType,
            IsActive = true
        };

        _memberRepository.AddMember(member);

        Console.WriteLine(
            "Member Added Successfully");
    }

    public void ViewMembers()
    {
        List<Member> members =
            _memberRepository.GetAllMembers();

        if (!members.Any())
        {
            Console.WriteLine(
                "No Members Found");

            return;
        }

        foreach (var member in members)
        {
            Console.WriteLine(
                $"{member.MemberId} | " +
                $"{member.FullName} | " +
                $"{member.Email} | " +
                $"{member.Phone} | " +
                $"{member.MembershipType} | " +
                $"{member.IsActive}");
        }
    }

    public void SearchMember()
    {
        Console.WriteLine(
            "1. Search By Email");

        Console.WriteLine(
            "2. Search By Phone");

        Console.Write("Enter Choice : ");

        if (!int.TryParse(
            Console.ReadLine(),
            out int choice))
        {
            Console.WriteLine(
                "Invalid Input");

            return;
        }

        Member? member = null;

        if (choice == 1)
        {
            Console.Write(
                "Enter Email : ");

            string email =
                Console.ReadLine()!;

            if (!ValidationHelper
                .IsValidEmail(email))
            {
                Console.WriteLine(
                    "Invalid Email");

                return;
            }

            member = _memberRepository
                .GetMemberByEmail(email);
        }
        else if (choice == 2)
        {
            Console.Write(
                "Enter Phone : ");

            string phone =
                Console.ReadLine()!;

            if (!ValidationHelper
                .IsValidPhone(phone))
            {
                Console.WriteLine(
                    "Invalid Phone Number");

                return;
            }

            member = _memberRepository
                .GetMemberByPhone(phone);
        }
        else
        {
            Console.WriteLine(
                "Invalid Choice");

            return;
        }

        if (member == null)
        {
            Console.WriteLine(
                "Member Not Found");

            return;
        }

        Console.WriteLine(
            $"{member.MemberId} | " +
            $"{member.FullName} | " +
            $"{member.Email} | " +
            $"{member.Phone} | " +
            $"{member.MembershipType}");
    }

    public void DeactivateMember()
    {
        Console.Write(
            "Enter Member Id : ");

        if (!int.TryParse(
            Console.ReadLine(),
            out int memberId))
        {
            Console.WriteLine(
                "Invalid Member Id");

            return;
        }

        Member? member =
            _memberRepository
            .GetMemberById(memberId);

        if (member == null)
        {
            Console.WriteLine(
                "Member Not Found");

            return;
        }

        if (!member.IsActive)
        {
            Console.WriteLine(
                "Member Already Inactive");

            return;
        }

        member.IsActive = false;

        _memberRepository
            .UpdateMember(member);

        Console.WriteLine(
            "Member Deactivated");
    }

    public void ActivateMember()
    {
        Console.Write(
            "Enter Member Id : ");

        if (!int.TryParse(
            Console.ReadLine(),
            out int memberId))
        {
            Console.WriteLine(
                "Invalid Member Id");

            return;
        }

        Member? member =
            _memberRepository
            .GetMemberById(memberId);

        if (member == null)
        {
            Console.WriteLine(
                "Member Not Found");

            return;
        }

        if (member.IsActive)
        {
            Console.WriteLine(
                "Member Already Active");

            return;
        }

        member.IsActive = true;

        _memberRepository
            .UpdateMember(member);

        Console.WriteLine(
            "Member Activated Successfully");
    }


    public void UpdateMembershipType()
    {
        Console.Write("Enter Member Id : ");

        if (!int.TryParse(
            Console.ReadLine(),
            out int memberId))
        {
            Console.WriteLine(
                "Invalid Member Id");

            return;
        }

        Member? member =
            _memberRepository
            .GetMemberById(memberId);

        if (member == null)
        {
            Console.WriteLine(
                "Member Not Found");

            return;
        }

        Console.WriteLine(
            $"Current Membership : " +
            $"{member.MembershipType}");

        Console.WriteLine("1. Basic");

        Console.WriteLine("2. Student");

        Console.WriteLine("3. Premium");

        Console.Write(
            "Choose New Membership Type : ");

        if (!int.TryParse(
            Console.ReadLine(),
            out int choice))
        {
            Console.WriteLine(
                "Invalid Choice");

            return;
        }

        if (choice < 1 || choice > 3)
        {
            Console.WriteLine(
                "Invalid Membership Type");

            return;
        }

        member.MembershipType =
            (MembershipType)choice;

        _memberRepository
            .UpdateMember(member);

        Console.WriteLine(
            "Membership Updated Successfully");
    }
}