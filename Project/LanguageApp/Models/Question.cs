using System.ComponentModel.DataAnnotations;

namespace LanguageApp.Models
{
    public class Question
    {
        [Key]
        public int QuestionId { get; set; }
        public int WordId { get; set; }
        
        public int QuizId { get; set; }
        
    }
}