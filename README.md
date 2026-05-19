# Community Library Membership & Book Lending System

## Overview

A console-based Library Management System built using:

* .NET 8
* Entity Framework Core
* PostgreSQL
* C#

The application manages library members, books, borrowing, returns, fines, and reports using proper layered architecture and business rules.

---

# Features

## Member Management

* Add Member
* View Members
* Search Member
* Activate / Deactivate Member
* Update Membership Type

## Book Management

* Add Categories
* Add Books
* Add Book Copies
* Search Books
* Mark Copy as Damaged

## Borrowing System

* Borrow Books
* Membership-based borrowing limits
* Duplicate borrowing prevention
* Fine validation
* Availability checking
* Transaction handling using EF Core

## Return & Fine Management

* Return borrowed books
* Automatic fine calculation
* Fine payment tracking
* View pending fines
* Fine history

## Reports

* Borrowed books
* Overdue books
* Members with pending fines
* Most borrowed books
* Available books by category
* Member borrowing history

---

# Membership Rules

| Membership Type | Max Books | Borrow Days |
| --------------- | --------- | ----------- |
| Basic           | 2         | 7           |
| Student         | 3         | 10          |
| Premium         | 5         | 15          |

Fine Rule:

* ₹10 per delayed day

Borrowing blocked if pending fine exceeds ₹500.

---

# Technologies Used

* .NET 8
* C#
* Entity Framework Core
* PostgreSQL
* Npgsql

---

# Project Structure

```text
Contexts
Models
Repository
Interfaces
Services
Enums
Utils
Exceptions
Presentation
Migrations
```

---

# Database Tables

* Members
* BookCategories
* Books
* BookCopies
* Borrowings
* FinePayments

---

# PostgreSQL Function

Implemented PostgreSQL function:

```sql
calculate_member_fine(member_id)
```

Used to calculate total unpaid fine of a member.

---

# Transaction Handling

Borrowing process uses EF Core transaction.

If any validation or database operation fails:

* Borrowing record rollback occurs
* Book copy status rollback occurs

---

# Validations

* Email validation
* Phone number validation
* Duplicate member checks
* Borrow limit validation
* Fine limit validation
* Book availability validation

---

# How to Run

## Apply Migrations

```bash
dotnet ef database update
```

## Run Application

```bash
dotnet run
```

---

# Sample Modules Flow

1. Add Category
2. Add Book
3. Add Book Copy
4. Add Member
5. Borrow Book
6. Return Book
7. Pay Fine
8. View Reports

---

# Conclusion

This project demonstrates:

* Layered Architecture
* Repository Pattern
* EF Core with PostgreSQL
* Transaction Handling
* Business Rule Implementation
* Fine Calculation Logic
* OOP Principles
