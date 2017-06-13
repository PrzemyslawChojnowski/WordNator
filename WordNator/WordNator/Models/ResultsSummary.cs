using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordNator.Models
{
    public class ResultsSummary
    {
        public int Id { get; set; }
        public LessonType LessonType { get; set; }
        public LessonLanguage LessonLanguage { get; set; }
        public int Try { get; set; }
        public int CorrectAnswersCount { get; set; }
        public int QuestionsCount { get; set; }

        public int LessonId { get; set; }
        public virtual Lesson Lesson { get; set; }
    }
}
