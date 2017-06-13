using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordNator.Models
{
    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string EnglishWord { get; set; }
        public string PolishWord { get; set; }

        public virtual ICollection<LessonQuestion> LessonQuestions { get; set; }
        public virtual ICollection<Result> Results { get; set; }

        public Question()
        {
            Results = new List<Result>();
            LessonQuestions = new List<LessonQuestion>();
        }
    }
}
