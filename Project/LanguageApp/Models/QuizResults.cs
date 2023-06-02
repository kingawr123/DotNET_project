using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageApp.Models
{
    public class QuizResults
    {
        public int points { get; set; }
        public List<Word> words { get; set; }
        Dictionary<int, string> answers { get; set; }
    }
}