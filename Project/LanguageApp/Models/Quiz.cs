using System.ComponentModel.DataAnnotations;

namespace LanguageApp.Models
{
    public class Quiz
    {
        [Key]
        public int QuizId { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Word")]
        public ICollection<Word> ?Words { get; set; }   
       
        public LoginVM User { get; set; }
    
    }
}