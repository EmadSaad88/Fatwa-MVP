# Fatwa Q&A System

An ASP.NET Core web application for managing Islamic questions and answers with a public submission form and secure admin control panel.

## Features

### Public Interface
- **Anonymous Question Submission**: Users can submit Islamic questions without registration
- **Browse Answered Questions**: View publicly published answers organized by category
- **Responsive Design**: Mobile-friendly interface using Bootstrap
- **Multiple Categories**: Questions organized by topics (Islamic Law, Daily Life, Family, Business, Health, etc.)

### Admin Control Panel
- **Secure Authentication**: Admin login with session-based security
- **Question Management**: 
  - Review pending questions
  - Provide detailed answers
  - Approve/reject questions
  - Manage publication status
- **User Management**: Create and manage admin accounts
- **Dashboard**: View statistics on total, pending, answered, and published questions
- **Password Management**: Secure password change functionality

### Database
- **SQLite Integration**: Lightweight, file-based database
- **Entity Framework Core**: Modern ORM for data access
- **Relational Schema**: Proper relationships between Questions and Users

## Technology Stack

- **Framework**: ASP.NET Core 10.0 (MVC)
- **Database**: SQLite with Entity Framework Core
- **Frontend**: Bootstrap 5, Razor Views
- **Authentication**: Session-based
- **Security**: SHA256 password hashing

## Project Structure

```
FatwaQA/
├── Controllers/
│   ├── QuestionsController.cs      # Public question submission & viewing
│   ├── AdminController.cs           # Admin panel & authentication
│   └── HomeController.cs            # Home page
├── Models/
│   ├── Question.cs                  # Question model
│   └── User.cs                      # Admin user model
├── Data/
│   └── FatwaContext.cs             # EF Core DbContext
├── Views/
│   ├── Questions/
│   │   ├── Submit.cshtml           # Question submission form
│   │   ├── SubmitSuccess.cshtml    # Success message
│   │   ├── Answered.cshtml         # List of answered questions
│   │   └── ViewAnswer.cshtml       # View single answer
│   ├── Admin/
│   │   ├── Login.cshtml            # Admin login
│   │   ├── Dashboard.cshtml        # Admin dashboard
│   │   ├── ManageQuestions.cshtml  # Question management
│   │   ├── AnswerQuestion.cshtml   # Answer editing form
│   │   ├── ManageUsers.cshtml      # User management
│   │   ├── CreateUser.cshtml       # Create new admin user
│   │   └── ChangePassword.cshtml   # Password change
│   ├── Home/
│   │   └── Index.cshtml            # Home page
│   └── Shared/
│       └── _Layout.cshtml          # Main layout template
├── Helpers/
│   └── DatabaseSeeder.cs           # Initial data seeding
├── appsettings.json                # Configuration
└── Program.cs                       # Application startup
```

## Installation & Setup

### Prerequisites
- .NET SDK 10.0 or later
- SQLite (included with .NET)

### Steps

1. **Navigate to project directory**:
   ```powershell
   cd FatwaQA
   ```

2. **Restore dependencies**:
   ```powershell
   dotnet restore
   ```

3. **Create database** (migrations already created):
   ```powershell
   dotnet ef database update
   ```

4. **Build the project**:
   ```powershell
   dotnet build
   ```

5. **Run the application**:
   ```powershell
   dotnet run
   ```

The application will start at `https://localhost:7123` (or `http://localhost:5123`)

## Default Admin Credentials

After initial setup, use these credentials to login to the admin panel:

- **URL**: https://localhost:7123/Admin/Login
- **Username**: `admin`
- **Password**: `Admin@123`

⚠️ **IMPORTANT**: Change the default password immediately after first login!

## Database Schema

### Questions Table
- `Id` (Primary Key)
- `Title` (Required)
- `Content` (Required)
- `Category` (Required)
- `SubmitterName` (Required)
- `SubmitterEmail` (Required)
- `Status` (Pending, Answered, Rejected)
- `SubmittedDate`
- `UpdatedDate`
- `Answer`
- `AnsweredById` (Foreign Key to Users)
- `IsPublished`

### Users Table
- `Id` (Primary Key)
- `Username` (Unique, Required)
- `Email` (Unique, Required)
- `PasswordHash` (Required)
- `FullName` (Required)
- `Role` (Admin, Moderator)
- `CreatedDate`
- `IsActive`

## Usage Guide

### For Public Users

1. **Submit a Question**:
   - Click "Ask Question" from the homepage
   - Fill in the form with title, content, and category
   - Enter your name and email
   - Submit the question

2. **View Answers**:
   - Click "Browse Answers" to see published answers
   - Click "View Answer" to read the full answer and question

### For Admin Users

1. **Login**:
   - Navigate to `/Admin/Login`
   - Enter credentials

2. **Review Questions**:
   - Go to Dashboard
   - Click "Review Pending Questions"
   - Click "View/Answer" on any question

3. **Provide Answer**:
   - Enter your answer in the text field
   - Check "Publish" to make it public
   - Click "Save Answer"

4. **Manage Users**:
   - From Dashboard, click "Manage Users"
   - Click "Create New User" to add admin accounts

5. **Change Password**:
   - Click admin menu dropdown
   - Select "Change Password"
   - Enter current and new password

## Security Considerations

- All passwords are hashed using SHA256
- Admin sessions expire after 30 minutes of inactivity
- Only authenticated admins can manage questions
- Questions require approval before publishing
- Email verification should be implemented in production

## Future Enhancements

- Email notifications for question submissions and answers
- Advanced search and filtering
- Category management interface
- Question editing for submitters
- Rating/feedback system
- Multi-language support
- API endpoints for external integration
- Email verification
- Captcha for form protection
- Audit logging

## Database

The application uses SQLite database stored as `fatwa.db` in the project root directory.

## Notes

- The application automatically seeds an admin user on first run
- Questions are not published until explicitly approved by an admin
- The default home page displays three main features
- Bootstrap 5 is used for responsive UI

## Support & Contact

For issues or questions about the system, contact the development team.

## License

This project is for demonstration and educational purposes.
