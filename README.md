MyLibraryPro - Library Management System

Overview

MyLibraryPro is a comprehensive desktop application for managing library operations, built with C# WinForms and SQL Server. The application provides a modern interface for librarians to manage books, borrowers, and book lending operations efficiently.

Features

1. User Authentication
- Secure login system with username/password validation
- Password masking for security
- Input validation for empty fields
- Database authentication against Users table

2. Book Management
-  CRUD Operations: Add, view, edit, and delete books
- Validation:
  - Required fields (Title, Author)
  - Numeric validation for Year and Available Copies
  - Year range validation (1000-current year)
  - Copies validation (must be ≥ 0)
- Search functionality by title or author
- DataGridView display with selection handling

 3. Borrower Management
- CRUD Operations: Add, view, edit, and delete borrowers
- Validation:
  - Required fields (Name)
  - Email format validation (basic pattern matching)
  - Phone number validation (digits only)
- Search functionality by name, email, or phone
- DataGridView display with selection handling

4. Book Transactions
- Issue Books:
  - Select from available books (only shows books with copies > 0)
  - Select from active borrowers
  - Date validation (due date cannot be before issue date)
  - Automatic decrement of available copies
- Return Books:
  - Select from issued books
  - Only allows return of unreturned books
  - Automatic increment of available copies
  - Visual indication of returned status

5. User Interface
- Modern styling with rounded corners and gradients
- Tab-based navigation for different sections
- Responsive design that works at different screen sizes
- Clear visual feedback for actions and errors
- Confirmation dialogs for destructive actions

 Database Schema

The application uses SQL Server LocalDB with the following tables:

1. Users- Stores login credentials
2. Books- Stores book information (Title, Author, Year, AvailableCopies)
3. Borrowers- Stores borrower information (Name, Email, Phone)
4. IssuedBooks - Tracks book lending transactions (BookID, BorrowerID, IssueDate, DueDate, Returned)

 Installation Instructions

Prerequisites
- Visual Studio 2019 or later
- .NET Framework 4.7.2
- SQL Server Express LocalDB (included with Visual Studio)

Setup Steps
1. Clone the repository or download the source code
2. Open the solution in Visual Studio
3. Build the solution (Ctrl+Shift+B)
4. Run the SQL script `LibraryDB.sql` (provided separately) to create the database
5. Run the application (F5)

 Default Credentials
- Username: `admin`
- Password: `admin123`


 Technical Stack
- Frontend: Windows Forms (WinForms) with C#
- Backend: SQL Server (LocalDB)
- Data Access: ADO.NET with parameterized queries
- Architecture: 2-tier (Presentation + Data)

 Screenshots

( screenshots of key screens are included in the documentation folder)
1. Login Screen
2. Book Management Tab
3. Borrower Management Tab
4. Book Issuing/Returning Section


 Future Enhancements
- Implement advanced reporting
- Add barcode scanning support
- Add book reservation system
- Implement fine calculation for overdue books

challenges

-while doing this project most of the conceptes and stratagies are new so we had to explore them before using so that is one of the challenges
-there were a lot of debugging in the process of making this project as an other major challenge
-since this project was done with other projects a little of time limitation was also a challange

used AI fearues

-deepseek- to guide how the project flows and explain codes for us
-gemini - to debug some issues on the code and learn from them 
