using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageApp.Models
{
    public class LoginVM
    {
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; }

        public string Password { get; set; }

        public ICollection<Quiz> Quizzes { get; set; }
    }
}