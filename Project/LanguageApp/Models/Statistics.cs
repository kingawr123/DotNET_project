using System.ComponentModel.DataAnnotations;

namespace LanguageApp.Models
{
    public class Statistics
    {
        [Key]
        public int StatisticsId { get; set; }
        public ICollection<User> UserId { get; set; }
        
        [Display(Name = "Quiz Counter")]
        public int QuizCounter { get; set; }   

        [Display(Name = "Average Score")]
        public double AverageScore { get; set; }
    }
}