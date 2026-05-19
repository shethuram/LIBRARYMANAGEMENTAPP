using LibraryManagementApp.Models;

namespace LibraryManagementApp.Interfaces.RepositoryInterfaces;

public interface IBorrowingRepository
{
    void AddBorrowing(Borrowing borrowing);

    Borrowing? GetBorrowingById(int borrowingId);

    List<Borrowing> GetActiveBorrowingsByMember(int memberId);

    bool HasActiveBorrowing(int memberId, int bookId);

    decimal GetPendingFine(int memberId);

    void UpdateBorrowing(Borrowing borrowing);
}