using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageApp.Models
{
    public class QuizDetails
    {
        public Quiz Quiz { get; set; }
        public List<Word> Words { get; set; }
    }
}