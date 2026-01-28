using System;

namespace FatwaQA.Models
{
    public class Question
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public required string Category { get; set; }
        public required string SubmitterName { get; set; }
        public required string SubmitterEmail { get; set; }
        public string Status { get; set; } = "Pending"; // Pending, Answered, Rejected
        public DateTime SubmittedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }
        public string? Answer { get; set; }
        public int? AnsweredById { get; set; }
        public User? AnsweredBy { get; set; }
        public bool IsPublished { get; set; } = false;
    }
}

