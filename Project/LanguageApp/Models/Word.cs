using System.ComponentModel.DataAnnotations;

namespace LanguageApp.Models
{
    public class Word
    {
        [Key]
        public int WordId { get; set; }
        [Display(Name = "Polski")]
        public String? Polish { get; set; }
        [Display(Name = "Tłumacznie")]
        public String? Translation { get; set; }

        public ICollection<Question> ?Questions { get; set; }

    }
}