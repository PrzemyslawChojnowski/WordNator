using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordNator.Models
{
    public enum LessonType { Spelling = 1, Speaking = 2 };
    public enum LessonLanguage { English = 1, Polish = 2 };

    public class Result
    {
        [Key]
        public int Id { get; set; }
        public bool AnswerCorrectness { get; set; }
        public string UserAnswer { get; set; }
        public int Try { get; set; }
        public LessonType LessonType { get; set; }
        public LessonLanguage LessonLanguage { get; set; }
        
        public int LessonId { get; set; }
        public int QuestionId { get; set; }

        [ForeignKey("QuestionId")]
        public virtual Question Question { get; set; }
    }
}
