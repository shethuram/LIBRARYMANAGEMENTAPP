using LibraryManagementApp.Services;

namespace LibraryManagementApp.Presentation;

public class MainMenu
{
    private readonly MemberService _memberService;

    private readonly BookService _bookService;

    private readonly BorrowingService _borrowingService;

    private readonly FineService _fineService;

    private readonly ReportService _reportService;

    public MainMenu()
    {
        _memberService = new MemberService();

        _bookService = new BookService();

        _borrowingService = new BorrowingService();

        _fineService = new FineService();

        _reportService = new ReportService();
    }

    public void ShowMenu()
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("====== LIBRARY MENU ======");

            Console.WriteLine("1. Member Management");

            Console.WriteLine("2. Book Management");

            Console.WriteLine("3. Borrow Book");

            Console.WriteLine("4. Return Book");

            Console.WriteLine("5. Fine Management");

            Console.WriteLine("6. Reports");

            Console.WriteLine("7. Exit");

            Console.Write("Enter Choice : ");

            if (!int.TryParse(
                Console.ReadLine(),
                out int choice))
            {
                Console.WriteLine(
                    "Invalid Input");

                continue;
            }

            switch (choice)
            {
                case 1:
                    MemberMenu();
                    break;

                case 2:
                    BookMenu();
                    break;

                case 3:
                    _borrowingService.BorrowBook();
                    break;

                case 4:
                    _borrowingService.ReturnBook();
                    break;

                case 5:
                    FineMenu();
                    break;

                case 6:
                    ReportMenu();
                    break;

                case 7:
                    return;

                default:
                    Console.WriteLine(
                        "Invalid Choice");
                    break;
            }
        }
    }

    private void MemberMenu()
    {
        Console.WriteLine();

        Console.WriteLine("1. Add Member");

        Console.WriteLine("2. View Members");

        Console.WriteLine("3. Search Member");

        Console.WriteLine("4. Deactivate Member");

        Console.WriteLine("5. Activate Member");

        Console.WriteLine("6. Update Membership Type");

        Console.Write("Enter Choice : ");

        if (!int.TryParse(
            Console.ReadLine(),
            out int choice))
        {
            Console.WriteLine(
                "Invalid Input");

            return;
        }

        switch (choice)
        {
            case 1:
                _memberService.AddMember();
                break;

            case 2:
                _memberService.ViewMembers();
                break;

            case 3:
                _memberService.SearchMember();
                break;

            case 4:
                _memberService.DeactivateMember();
                break;

            case 5:
                _memberService.ActivateMember();
                break;

            case 6:
                _memberService.UpdateMembershipType();
                break;

            default:
                Console.WriteLine(
                    "Invalid Choice");
                break;
        }
    }

    private void BookMenu()
    {
        Console.WriteLine();

        Console.WriteLine("1. Add Category");

        Console.WriteLine("2. Add Book");

        Console.WriteLine("3. Add Book Copy");

        Console.WriteLine("4. View Books");

        Console.WriteLine("5. Search Books");

        Console.WriteLine("6. Mark Copy Damaged");

        Console.Write("Enter Choice : ");

        if (!int.TryParse(
            Console.ReadLine(),
            out int choice))
        {
            Console.WriteLine(
                "Invalid Input");

            return;
        }

        switch (choice)
        {
            case 1:
                _bookService.AddCategory();
                break;

            case 2:
                _bookService.AddBook();
                break;

            case 3:
                _bookService.AddBookCopy();
                break;

            case 4:
                _bookService.ViewBooks();
                break;

            case 5:
                _bookService.SearchBooks();
                break;

            case 6:
                _bookService.MarkCopyDamaged();
                break;

            default:
                Console.WriteLine(
                    "Invalid Choice");
                break;
        }
    }

    private void FineMenu()
    {
        Console.WriteLine();

        Console.WriteLine("1. View Pending Fines");

        Console.WriteLine("2. Pay Fine");

        Console.WriteLine("3. Fine History");

        Console.WriteLine(
            "4. Get Fine Using PostgreSQL Function");

        Console.Write("Enter Choice : ");

        if (!int.TryParse(
            Console.ReadLine(),
            out int choice))
        {
            Console.WriteLine(
                "Invalid Input");

            return;
        }

        switch (choice)
        {
            case 1:
                _fineService.ViewPendingFines();
                break;

            case 2:
                _fineService.PayFine();
                break;

            case 3:
                _fineService.FineHistory();
                break;

            case 4:
                _fineService.GetFineUsingFunction();
                break;

            default:
                Console.WriteLine(
                    "Invalid Choice");
                break;
        }
    }

    private void ReportMenu()
    {
        Console.WriteLine();

        Console.WriteLine("1. Borrowed Books");

        Console.WriteLine("2. Overdue Books");

        Console.WriteLine("3. Members With Pending Fines");

        Console.WriteLine("4. Most Borrowed Books");

        Console.WriteLine("5. Available Books By Category");

        Console.WriteLine("6. Member Borrow History");

        Console.Write("Enter Choice : ");

        if (!int.TryParse(
            Console.ReadLine(),
            out int choice))
        {
            Console.WriteLine(
                "Invalid Input");

            return;
        }

        switch (choice)
        {
            case 1:
                _reportService.BorrowedBooksReport();
                break;

            case 2:
                _reportService.OverdueBooksReport();
                break;

            case 3:
                _reportService.PendingFineMembersReport();
                break;

            case 4:
                _reportService.MostBorrowedBooksReport();
                break;

            case 5:
                _reportService.AvailableBooksByCategory();
                break;

            case 6:
                _reportService.MemberBorrowHistory();
                break;

            default:
                Console.WriteLine(
                    "Invalid Choice");
                break;
        }
    }
}