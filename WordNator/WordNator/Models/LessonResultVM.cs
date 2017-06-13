using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordNator.Models
{
    public class LessonResultVM
    {
        public string AnswerCorrectness { get; set; }
        public string UserAnswer { get; set; }
        public string Question { get; set; }
    }
}
