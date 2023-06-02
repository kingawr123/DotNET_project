using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LanguageApp.Data;
using LanguageApp.Models;

namespace LanguageApp.Controllers
{
    public class QuizController : Controller
    {
        private readonly LanguageAppContext _context;

        public QuizController(LanguageAppContext context)
        {
            _context = context;
        }

        // GET: Quiz
        public async Task<IActionResult> Index()
        {
              return _context.Quiz != null ? 
                          View(await _context.Quiz.ToListAsync()) :
                          Problem("Entity set 'LanguageAppContext.Quiz'  is null.");
        }

        // GET: Quiz/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Quiz == null)
            {
                return NotFound();
            }

            var quiz = await _context.Quiz
                .FirstOrDefaultAsync(m => m.QuizId == id);
            if (quiz == null)
            {
                return NotFound();
            }
            var wordIds = _context.Question.Where(m => m.QuizId == id).Select(m => m.WordId).ToList();
            foreach (var wordId in wordIds){
                Console.WriteLine(wordId);
            }
            var words = _context.Word.Where(w => wordIds.Contains(w.WordId)).ToList();
            QuizDetails details = new QuizDetails { Quiz = quiz, Words = words };

            return View(details);
        }

        // GET: Quiz/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Quiz/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QuizId,Name,Description,Liczba")] Quiz quiz)
        {
            if (ModelState.IsValid)
            {
                var words = _context.Word.ToList();
                int id = _context.Question.Count();

                Random rnd = new Random();
                List<int> wordIds = words.Select(w => w.WordId).ToList();

                _context.Add(quiz);
                await _context.SaveChangesAsync();


                for (int i = 0; i < quiz.Liczba; i++)
                {
                    int randomWordIndex = rnd.Next(words.Count);
                    int wordId = words[randomWordIndex].WordId;

                    // Check if the generated wordId exists in the wordIds list
                    while (!wordIds.Contains(wordId))
                    {
                        randomWordIndex = rnd.Next(words.Count);
                        wordId = words[randomWordIndex].WordId;
                    }

                    Question question = new Question
                    {
                        //QuestionId = ++id, 
                        WordId = wordId,
                        QuizId = quiz.QuizId
                    };

                    _context.Question.Add(question);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }

            return View(quiz);
        }

        // GET: Quiz/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Quiz == null)
            {
                return NotFound();
            }

            var quiz = await _context.Quiz.FindAsync(id);
            if (quiz == null)
            {
                return NotFound();
            }
            return View(quiz);
        }

        // POST: Quiz/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("QuizId,Name,Description,Liczba")] Quiz quiz)
        {
            if (id != quiz.QuizId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quiz);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuizExists(quiz.QuizId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(quiz);
        }

        // GET: Quiz/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Quiz == null)
            {
                return NotFound();
            }

            var quiz = await _context.Quiz
                .FirstOrDefaultAsync(m => m.QuizId == id);
            if (quiz == null)
            {
                return NotFound();
            }

            return View(quiz);
        }

        // POST: Quiz/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Quiz == null)
            {
                return Problem("Entity set 'LanguageAppContext.Quiz'  is null.");
            }
            var quiz = await _context.Quiz.FindAsync(id);
            if (quiz != null)
            {
                _context.Quiz.Remove(quiz);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuizExists(int id)
        {
          return (_context.Quiz?.Any(e => e.QuizId == id)).GetValueOrDefault();
        }

        // GET: Quiz/Details/5
        public async Task<IActionResult> TakeQuiz(int? id)
        {
            if (id == null || _context.Quiz == null)
            {
                return NotFound();
            }

            var quiz = await _context.Quiz
                .FirstOrDefaultAsync(m => m.QuizId == id);
            if (quiz == null)
            {
                return NotFound();
            }
            var wordIds = _context.Question.Where(m => m.QuizId == id).Select(m => m.WordId).ToList();
            foreach (var wordId in wordIds){
                Console.WriteLine(wordId);
            }
            var words = _context.Word.Where(w => wordIds.Contains(w.WordId)).ToList();
            QuizDetails details = new QuizDetails { Quiz = quiz, Words = words };

            return View(details);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult FinishQuiz(Dictionary<int, string> answers, int[] wordIds)
        {
            // Perform the necessary logic to evaluate the answers and calculate points
            int points = 0;
            for (int i = 0; i < wordIds.Length; i++)
            {
                int wordId = wordIds[i];
                string answer = answers[wordId];
                
                var word = _context.Word.FirstOrDefault(w => w.WordId == wordId);
                if (word != null && answer == word.Translation)
                {
                    points++;
                }
            }

            var words = _context.Word.Where(w => wordIds.Contains(w.WordId)).ToList();
            List<WordSummary> wordsSummary = new List<WordSummary>();
            for (int i=0; i<words.Count; i++){
                wordsSummary.Add(new WordSummary { 
                    Polish = words[i].Polish, 
                    Answer = words[i].Translation, 
                    YourAnswer = answers[words[i].WordId]});
            }
            
            // You can store the points in the database or use them as needed
            // For demonstration purposes, we will pass the points to the view
            QuizResults results = new QuizResults { Points = points, Words = wordsSummary };

            var userIdString = HttpContext.Session.GetString("UserId");
            if (int.TryParse(userIdString, out int userId))
            {
                var statistic = _context.Statistics.FirstOrDefault(s => s.StatisticsId == userId);
                if (statistic != null)
                {

                     double pointsValue = Convert.ToDouble(points);
                    int wordsCount = words.Count;

                    if (wordsCount > 0)
                    {
                        double averageScore = (statistic.QuizCounter * statistic.AverageScore + pointsValue / wordsCount) / (statistic.QuizCounter + 1);
                        statistic.AverageScore = averageScore;
                    }
                    else
                    {
                        statistic.AverageScore = 0; // Handle the case when wordsCount is 0
                    }
                    statistic.QuizCounter = statistic.QuizCounter + 1;
                    _context.SaveChanges();
                }
                else
                {
                    statistic = new Statistics { StatisticsId =  int.Parse(HttpContext.Session.GetString("UserId")), 
                    QuizCounter = 1,
                    AverageScore = points/words.Count};
                    _context.Statistics.Add(statistic);
                    _context.SaveChanges();
                }
            }
            else
            {
                return RedirectToAction("Index");
            }

            
            // You can redirect to a results view or perform any other action
            return View(results);
        }
       
    }
}
