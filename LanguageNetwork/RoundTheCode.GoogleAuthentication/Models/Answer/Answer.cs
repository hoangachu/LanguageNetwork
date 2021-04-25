using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageNetwork.Models.Answer
{
    public class Answer
    {
        public int AnswerID { get; set; }
        public int? QuestionID { get; set; }
        public string AnswerText { get; set; }
        public int? UserID { get; set; }
        public DateTime? AnswerTime { get; set; }
    }
}
