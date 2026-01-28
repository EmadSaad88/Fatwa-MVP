# Fatwa Q&A System

An ASP.NET Core MVC web application for managing Islamic questions and answers with a public submission form and secure admin control panel.

## 📋 Overview

This is a complete question-and-answer system designed specifically for Islamic knowledge sharing. It features:

- **Public Interface**: Anonymous question submission and browsing of answered questions
- **Admin Control Panel**: Secure authentication, question management, and answer publishing
- **SQLite Database**: Lightweight, file-based persistence with Entity Framework Core
- **Responsive Design**: Mobile-friendly Bootstrap 5 interface

## ✨ Features

### Public Features
- 📝 **Anonymous Question Submission** - Submit Islamic questions without registration
- 🔍 **Browse Answers** - View answered questions organized by category
- 🏷️ **Category Organization** - Islamic Law, Daily Life, Family, Business, Health, etc.
- 📱 **Responsive Design** - Works on desktop, tablet, and mobile devices

### Admin Features
- 🔐 **Secure Login** - Session-based authentication with SHA256 password hashing
- 📊 **Dashboard** - Statistics on pending, answered, and published questions
- ✍️ **Answer Management** - Review, answer, approve/reject, and publish questions
- 👥 **User Management** - Create and manage admin accounts
- 🔒 **Password Management** - Secure password change functionality
- 🌐 **Publication Control** - Choose which answers to publish publicly

## 🛠️ Technology Stack

| Component | Technology |
|-----------|-----------|
| Framework | ASP.NET Core 10.0 MVC |
| Database | SQLite with Entity Framework Core |
| Frontend | Bootstrap 5, Razor Views |
| Authentication | Session-based |
| Security | SHA256 password hashing |
| ORM | Entity Framework Core |

## 📁 Project Structure

```
Fatwa_MVP/
├── FatwaQA/                          # Main project directory
│   ├── Controllers/
│   │   ├── QuestionsController.cs    # Public Q&A endpoints
│   │   ├── AdminController.cs        # Admin panel & authentication
│   │   └── HomeController.cs         # Home page
│   ├── Models/
│   │   ├── Question.cs               # Question entity
│   │   └── User.cs                   # Admin user entity
│   ├── Data/
│   │   └── FatwaContext.cs          # EF Core DbContext
│   ├── Views/
│   │   ├── Questions/                # Public question pages
│   │   ├── Admin/                    # Admin panel pages
│   │   ├── Home/                     # Home page
│   │   └── Shared/                   # Shared layout & components
│   ├── Helpers/
│   │   └── DatabaseSeeder.cs        # Initial data seeding
│   ├── Migrations/                   # EF Core migrations
│   ├── Properties/                   # Launch settings
│   ├── wwwroot/                      # Static files
│   ├── appsettings.json             # Configuration
│   ├── Program.cs                    # Application startup
│   ├── README.md                     # Detailed documentation
│   ├── QUICKSTART.md                 # Quick start guide
│   └── TESTING_GUIDE.md              # Testing instructions
├── .gitignore                        # Git ignore rules
├── .gitattributes                    # Git attributes
└── README.md                         # This file
```

## 🚀 Getting Started

### Prerequisites
- .NET SDK 10.0 or later
- Git
- A code editor (VS Code, Visual Studio, etc.)

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/yourusername/Fatwa_MVP.git
   cd Fatwa_MVP/FatwaQA
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Create the database**
   ```bash
   dotnet ef database update
   ```

4. **Build the project**
   ```bash
   dotnet build
   ```

5. **Run the application**
   ```bash
   dotnet run
   ```

6. **Access the application**
   - Home: `http://localhost:5086` or `https://localhost:7123`
   - Admin: `http://localhost:5086/Admin/Login`

## 🔐 Default Credentials

After first run, login to admin panel with:

| Field | Value |
|-------|-------|
| **URL** | `/Admin/Login` |
| **Username** | `admin` |
| **Password** | `Admin@123` |

⚠️ **IMPORTANT**: Change the default password immediately after first login!

## 📚 Database Schema

### Questions Table
```
Id (Primary Key)
Title (Required) - Question title
Content (Required) - Question details
Category (Required) - Topic category
SubmitterName (Required) - Asker's name
SubmitterEmail (Required) - Asker's email
Status (Pending/Answered/Rejected)
SubmittedDate
UpdatedDate
Answer - Admin's response
AnsweredById (Foreign Key)
IsPublished - Controls public visibility
```

### Users Table
```
Id (Primary Key)
Username (Unique, Required)
Email (Unique, Required)
PasswordHash (Required) - SHA256 hashed
FullName (Required)
Role (Admin/Moderator)
CreatedDate
IsActive
```

## 🧭 Main Routes

### Public Routes
| Route | Description |
|-------|-----------|
| `/` | Home page |
| `/Questions/Submit` | Submit a question |
| `/Questions/Answered` | Browse answered questions |
| `/Questions/ViewAnswer/{id}` | View specific answer |

### Admin Routes
| Route | Description |
|-------|-----------|
| `/Admin/Login` | Login page |
| `/Admin/Dashboard` | Admin dashboard |
| `/Admin/ManageQuestions` | Question management |
| `/Admin/AnswerQuestion/{id}` | Answer a question |
| `/Admin/ManageUsers` | User management |
| `/Admin/CreateUser` | Create admin user |
| `/Admin/ChangePassword` | Change password |
| `/Admin/Logout` | Logout |

## 🔧 Configuration

### appsettings.json
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=fatwa.db"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

## 📖 Documentation

- [README.md](./FatwaQA/README.md) - Detailed feature documentation
- [QUICKSTART.md](./FatwaQA/QUICKSTART.md) - Quick start guide
- [TESTING_GUIDE.md](./FatwaQA/TESTING_GUIDE.md) - Testing instructions

## 🧪 Testing

### Manual Testing
1. Submit a test question from the public form
2. Login as admin and answer the question
3. Check the "Publish" checkbox
4. Verify the answer appears in "Browse Answers"

### Test Data
The system auto-seeds with:
- Default admin user (username: `admin`, password: `Admin@123`)
- Empty questions table

## 🔒 Security Features

- ✅ SHA256 password hashing (no plaintext passwords)
- ✅ Session-based authentication with 30-minute timeout
- ✅ Server-side validation on all forms
- ✅ SQL injection prevention via Entity Framework
- ✅ CSRF protection via ASP.NET Core
- ✅ Secure password change functionality
- ✅ Admin-only access to control panel

## 📈 Future Enhancements

- [ ] Email notifications for submissions and answers
- [ ] Advanced search and filtering
- [ ] Category management interface
- [ ] User ratings and feedback system
- [ ] Multi-language support
- [ ] API endpoints for integrations
- [ ] Email verification
- [ ] CAPTCHA protection
- [ ] Audit logging
- [ ] Backup and restore functionality

## 🐛 Known Issues

None currently reported. Please create an issue if you find any bugs.

## 💡 Tips

- **For Development**: Use `dotnet watch run` to automatically rebuild on file changes
- **For Database**: Use a SQLite browser extension to inspect the database
- **For Styling**: Modify Bootstrap classes in Razor views
- **For Security**: Always change default admin password in production

## 📝 License

This project is provided as-is for educational and demonstration purposes.

## 👥 Author

Created with ASP.NET Core 10.0

## 🤝 Contributing

To contribute to this project:
1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📧 Support

For support and questions about this project, please open an issue on GitHub.

---

**Ready to use!** Start by running `dotnet run` in the FatwaQA directory. 🚀
