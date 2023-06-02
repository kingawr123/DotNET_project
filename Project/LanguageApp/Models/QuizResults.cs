using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageApp.Models
{
    public class QuizResults
    {
        public int Points { get; set; }
        public List<WordSummary> Words { get; set; }
    }
}