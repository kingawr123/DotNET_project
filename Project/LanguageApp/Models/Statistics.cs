using System.ComponentModel.DataAnnotations;

namespace LanguageApp.Models
{
    public class Statistics
    {
        [Key]
        [Display(Name = "Id Użytkownika")]

        public int StatisticsId { get; set; }
        
        [Display(Name = "Liczba wykonanych quizów")] 
        public int QuizCounter { get; set; }   

        [Display(Name = "Średnia wyników")]
        public double AverageScore { get; set; }
    }
}