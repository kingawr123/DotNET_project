using System.ComponentModel.DataAnnotations;

namespace LanguageApp.Models
{
    public class Question
    {
        [Key]
        public int QuestionId { get; set; }

        [Display(Name = "Word")]
        public int WordId { get; set; }
        
        [Display(Name = "Quiz")]
        public int QuizId { get; set; }
        
    }
}