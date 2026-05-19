using LibraryManagementApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementApp.Contexts;

public class LibraryDbContext : DbContext
{
    // Tables

    public DbSet<Member> Members => Set<Member>();

    public DbSet<Book> Books => Set<Book>();

    public DbSet<BookCategory> BookCategories => Set<BookCategory>();

    public DbSet<BookCopy> BookCopies => Set<BookCopy>();

    public DbSet<Borrowing> Borrowings => Set<Borrowing>();

    public DbSet<FinePayment> FinePayments => Set<FinePayment>();

    protected override void OnConfiguring(
        DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(
            "Host=localhost;Port=5432;Database=LibraryDB;Username=postgres;Password=Shethu@2128");
    }


    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        // Unique Constraints

        modelBuilder.Entity<Member>()
            .HasIndex(m => m.Email)
            .IsUnique();

        modelBuilder.Entity<Member>()
            .HasIndex(m => m.Phone)
            .IsUnique();

        modelBuilder.Entity<BookCategory>()
            .HasIndex(c => c.CategoryName)
            .IsUnique();

        modelBuilder.Entity<BookCopy>()
            .HasIndex(c => c.CopyCode)
            .IsUnique();


        // Relationships

        modelBuilder.Entity<Book>()
            .HasOne(b => b.BookCategory)
            .WithMany(c => c.Books)
            .HasForeignKey(b => b.BookCategoryId);

        modelBuilder.Entity<BookCopy>()
            .HasOne(c => c.Book)
            .WithMany(b => b.BookCopies)
            .HasForeignKey(c => c.BookId);

        modelBuilder.Entity<Borrowing>()
            .HasOne(b => b.Member)
            .WithMany(m => m.Borrowings)
            .HasForeignKey(b => b.MemberId);

        modelBuilder.Entity<Borrowing>()
            .HasOne(b => b.BookCopy)
            .WithMany(c => c.Borrowings)
            .HasForeignKey(b => b.BookCopyId);

        modelBuilder.Entity<FinePayment>()
            .HasOne(f => f.Borrowing)
            .WithMany(b => b.FinePayments)
            .HasForeignKey(f => f.BorrowingId);
    }
}