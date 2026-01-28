using Microsoft.AspNetCore.Mvc;
using FatwaQA.Data;
using FatwaQA.Models;

namespace FatwaQA.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly FatwaContext _context;

        public QuestionsController(FatwaContext context)
        {
            _context = context;
        }

        // Public form - anonymous submission
        [HttpGet]
        public IActionResult Submit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Submit(Question question)
        {
            if (ModelState.IsValid)
            {
                question.SubmittedDate = DateTime.Now;
                question.Status = "Pending";
                question.IsPublished = false;

                _context.Questions.Add(question);
                _context.SaveChanges();

                return RedirectToAction("SubmitSuccess");
            }

            return View(question);
        }

        public IActionResult SubmitSuccess()
        {
            return View();
        }

        // List answered questions
        public IActionResult Answered()
        {
            var answeredQuestions = _context.Questions
                .Where(q => q.Status == "Answered" && q.IsPublished)
                .OrderByDescending(q => q.UpdatedDate)
                .ToList();

            return View(answeredQuestions);
        }

        // View specific answered question
        public IActionResult ViewAnswer(int id)
        {
            var question = _context.Questions.FirstOrDefault(q => q.Id == id);
            
            if (question == null || question.Status != "Answered" || !question.IsPublished)
            {
                return NotFound();
            }

            return View(question);
        }
    }
}
