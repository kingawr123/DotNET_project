using System.ComponentModel.DataAnnotations;

namespace LanguageApp.Models
{
    public class Quiz
    {
        [Key]
        public int QuizId { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string? Name { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Required]
        [Display(Name = "Liczba pytań")]
        public int? Liczba { get; set; }
    
    }
}