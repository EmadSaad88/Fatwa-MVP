using Microsoft.AspNetCore.Mvc;
using FatwaQA.Data;
using FatwaQA.Models;
using System.Security.Cryptography;
using System.Text;

namespace FatwaQA.Controllers
{
    public class AdminController : Controller
    {
        private readonly FatwaContext _context;
        private const string AdminSessionKey = "AdminId";
        private const string AdminNameKey = "AdminName";

        public AdminController(FatwaContext context)
        {
            _context = context;
        }

        private bool IsAdminLoggedIn()
        {
            return HttpContext.Session.GetInt32(AdminSessionKey).HasValue;
        }

        private void RequireLogin()
        {
            if (!IsAdminLoggedIn())
            {
                RedirectToAction("Login").ExecuteResultAsync(ControllerContext);
            }
        }

        // Login page
        [HttpGet]
        public IActionResult Login()
        {
            if (IsAdminLoggedIn())
            {
                return RedirectToAction("Dashboard");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username && u.IsActive);

            if (user != null && VerifyPassword(password, user.PasswordHash))
            {
                HttpContext.Session.SetInt32(AdminSessionKey, user.Id);
                HttpContext.Session.SetString(AdminNameKey, user.FullName);
                return RedirectToAction("Dashboard");
            }

            ModelState.AddModelError("", "Invalid username or password");
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        // Dashboard
        public IActionResult Dashboard()
        {
            if (!IsAdminLoggedIn())
                return RedirectToAction("Login");

            var stats = new
            {
                TotalQuestions = _context.Questions.Count(),
                PendingQuestions = _context.Questions.Count(q => q.Status == "Pending"),
                AnsweredQuestions = _context.Questions.Count(q => q.Status == "Answered"),
                PublishedQuestions = _context.Questions.Count(q => q.IsPublished)
            };

            return View(stats);
        }

        // Questions management
        public IActionResult ManageQuestions(string status = "Pending")
        {
            if (!IsAdminLoggedIn())
                return RedirectToAction("Login");

            IEnumerable<Question> questions = _context.Questions.AsEnumerable();

            if (!string.IsNullOrEmpty(status))
            {
                questions = questions.Where(q => q.Status == status);
            }

            questions = questions.OrderByDescending(q => q.SubmittedDate);
            return View(questions);
        }

        // View and answer question
        [HttpGet]
        public IActionResult AnswerQuestion(int id)
        {
            if (!IsAdminLoggedIn())
                return RedirectToAction("Login");

            var question = _context.Questions.FirstOrDefault(q => q.Id == id);
            if (question == null)
                return NotFound();

            return View(question);
        }

        [HttpPost]
        public IActionResult AnswerQuestion(int id, string answer, string publish = "false")
        {
            if (!IsAdminLoggedIn())
                return RedirectToAction("Login");

            var question = _context.Questions.FirstOrDefault(q => q.Id == id);
            if (question == null)
                return NotFound();

            var adminIdValue = HttpContext.Session.GetInt32(AdminSessionKey);
            if (!adminIdValue.HasValue)
                return RedirectToAction("Login");

            // Parse the publish value - handle both "true" string and boolean
            bool isPublished = !string.IsNullOrEmpty(publish) && (publish.Equals("true", StringComparison.OrdinalIgnoreCase) || publish.Equals("on"));

            question.Answer = answer;
            question.Status = "Answered";
            question.AnsweredById = adminIdValue.Value;
            question.UpdatedDate = DateTime.Now;
            question.IsPublished = isPublished;

            _context.SaveChanges();

            return RedirectToAction("ManageQuestions", new { status = "Answered" });
        }

        // Publish question
        [HttpPost]
        public IActionResult PublishQuestion(int id)
        {
            if (!IsAdminLoggedIn())
                return RedirectToAction("Login");

            var question = _context.Questions.FirstOrDefault(q => q.Id == id);
            if (question != null)
            {
                question.IsPublished = true;
                _context.SaveChanges();
            }

            return RedirectToAction("ManageQuestions", new { status = "Answered" });
        }

        // Reject question
        [HttpPost]
        public IActionResult RejectQuestion(int id)
        {
            if (!IsAdminLoggedIn())
                return RedirectToAction("Login");

            var question = _context.Questions.FirstOrDefault(q => q.Id == id);
            if (question != null)
            {
                question.Status = "Rejected";
                question.UpdatedDate = DateTime.Now;
                _context.SaveChanges();
            }

            return RedirectToAction("ManageQuestions", new { status = "Pending" });
        }

        // User management
        public IActionResult ManageUsers()
        {
            if (!IsAdminLoggedIn())
                return RedirectToAction("Login");

            var users = _context.Users.OrderBy(u => u.Username).ToList();
            return View(users);
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            if (!IsAdminLoggedIn())
                return RedirectToAction("Login");

            return View();
        }

        [HttpPost]
        public IActionResult CreateUser(User user, string password)
        {
            if (!IsAdminLoggedIn())
                return RedirectToAction("Login");

            if (ModelState.IsValid)
            {
                // Check if username exists
                if (_context.Users.Any(u => u.Username == user.Username))
                {
                    ModelState.AddModelError("Username", "Username already exists");
                    return View(user);
                }

                user.PasswordHash = HashPassword(password);
                user.CreatedDate = DateTime.Now;
                user.IsActive = true;

                _context.Users.Add(user);
                _context.SaveChanges();

                return RedirectToAction("ManageUsers");
            }

            return View(user);
        }

        // Password management
        [HttpGet]
        public IActionResult ChangePassword()
        {
            if (!IsAdminLoggedIn())
                return RedirectToAction("Login");

            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(string currentPassword, string newPassword, string confirmPassword)
        {
            if (!IsAdminLoggedIn())
                return RedirectToAction("Login");

            if (newPassword != confirmPassword)
            {
                ModelState.AddModelError("", "New passwords do not match");
                return View();
            }

            var userIdValue = HttpContext.Session.GetInt32(AdminSessionKey);
            if (!userIdValue.HasValue)
                return RedirectToAction("Login");

            var user = _context.Users.FirstOrDefault(u => u.Id == userIdValue.Value);

            if (user == null || !VerifyPassword(currentPassword, user.PasswordHash))
            {
                ModelState.AddModelError("", "Current password is incorrect");
                return View();
            }

            user.PasswordHash = HashPassword(newPassword);
            _context.SaveChanges();

            return RedirectToAction("Dashboard");
        }

        // Helper methods
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        private bool VerifyPassword(string password, string hash)
        {
            var hashOfInput = HashPassword(password);
            return hashOfInput == hash;
        }
    }
}
