# Fatwa Q&A System - Quick Start Guide

## 🚀 Getting Started

### Step 1: Navigate to Project
```powershell
cd C:\Users\esaad294\Documents\Fatwa_MVP\FatwaQA
```

### Step 2: Run the Application
```powershell
dotnet run
```

The application will start and display something like:
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:7123
      Now listening on: http://localhost:5123
```

### Step 3: Access the Application

**Home Page**: https://localhost:7123 (or http://localhost:5123)

## 🔐 Admin Login

**URL**: https://localhost:7123/Admin/Login

**Credentials**:
- Username: `admin`
- Password: `Admin@123`

⚠️ **Change the default password immediately!**

## 📋 Main Routes

| Route | Purpose |
|-------|---------|
| `/` | Home page |
| `/Questions/Submit` | Submit a question (public) |
| `/Questions/Answered` | Browse answered questions |
| `/Questions/ViewAnswer/{id}` | View a specific answer |
| `/Admin/Login` | Admin login page |
| `/Admin/Dashboard` | Admin dashboard |
| `/Admin/ManageQuestions` | Manage questions |
| `/Admin/ManageUsers` | Manage admin users |

## 💾 Database

- **Location**: `fatwa.db` (in project root)
- **Type**: SQLite
- **Initialized**: Automatically on first run
- **Default Admin**: Auto-seeded on startup

## ✨ Key Features Implemented

✅ **Public Interface**
- Anonymous question submission form
- Browse answered questions by category
- View detailed answers with scholar information
- Responsive mobile-friendly design

✅ **Admin Panel**
- Secure login with session management
- Review and answer pending questions
- Publish/reject questions
- Manage admin users
- Password management
- Dashboard with statistics

✅ **Database**
- SQLite with Entity Framework Core
- Proper relationships between Questions and Users
- Indexes for performance
- Migrations for schema management

✅ **Security**
- SHA256 password hashing
- Session-based authentication
- Server-side validation
- Secure password change feature

## 🎯 Testing the System

### Test Question Submission
1. Go to https://localhost:7123/
2. Click "Ask Question"
3. Fill out the form
4. Submit

### Test Admin Features
1. Login at `/Admin/Login` with admin credentials
2. Click "Review Pending Questions"
3. Select a question and provide an answer
4. Check "Publish" and save
5. Go back to public site to see the answer

## 📁 Project Structure

```
Controllers/
  ├── QuestionsController.cs     # Public Q&A
  ├── AdminController.cs          # Admin panel
  └── HomeController.cs           # Home page

Models/
  ├── Question.cs                 # Question entity
  └── User.cs                     # Admin user entity

Views/
  ├── Questions/                  # Public pages
  ├── Admin/                       # Admin pages
  └── Shared/                      # Shared layout

Data/
  └── FatwaContext.cs            # Database context

Helpers/
  └── DatabaseSeeder.cs          # Initial data
```

## 🔧 Common Commands

```powershell
# Build the project
dotnet build

# Run the project
dotnet run

# Create a new migration
dotnet ef migrations add MigrationName

# Apply migrations to database
dotnet ef database update

# Open database in browser (SQLite viewer)
# Use a SQLite browser extension or tool
```

## 📝 Features to Explore

1. **Public Question Submission**
   - Submit a test question
   - Check confirmation page
   - Question appears in admin panel as "Pending"

2. **Admin Question Management**
   - Review pending questions
   - Provide a detailed answer
   - Choose to publish or reject
   - View in public site

3. **User Management**
   - Create additional admin users
   - Manage admin accounts

4. **Password Security**
   - Change admin password
   - Verify new password works on next login

## 🐛 Troubleshooting

**Issue**: Port already in use
**Solution**: Change port in launchSettings.json or use `dotnet run --urls "https://localhost:7124"`

**Issue**: Database locked
**Solution**: Ensure no other instance is running the app

**Issue**: Login fails
**Solution**: Make sure database seeding completed by checking `fatwa.db` exists

## 📚 Database Structure

### Questions Table
- Title, Content (question text)
- Category (Islamic Law, Daily Life, etc.)
- SubmitterName, SubmitterEmail (public name, email)
- Status (Pending, Answered, Rejected)
- Answer (admin-provided answer)
- IsPublished (public visibility)
- SubmittedDate, UpdatedDate
- AnsweredById (links to admin user)

### Users Table
- Username, Email (unique)
- PasswordHash (SHA256)
- FullName, Role (Admin/Moderator)
- IsActive, CreatedDate

## ✅ Verification Checklist

- [x] Project builds without errors
- [x] Database created automatically
- [x] Default admin user seeded
- [x] Public question form works
- [x] Admin login works
- [x] Question management works
- [x] Answer publishing works
- [x] UI is responsive and styled
- [x] All controllers functional
- [x] Session management working

## 🎓 Next Steps

1. Change the default admin password
2. Create additional admin accounts if needed
3. Test the complete workflow
4. Customize branding/styling as needed
5. Set up email notifications (future enhancement)
6. Deploy to production environment

---

**System Ready for Use!** 🎉
