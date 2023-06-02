using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageApp.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Display(Name = "Login")]
        public string Username { get; set; }

        [Display(Name = "Hasło")]
        public string Password { get; set; }

        public ICollection<Quiz>? Quizzes { get; set; }
    }
}