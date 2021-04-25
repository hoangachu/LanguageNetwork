using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LanguageNetwork.Models.Tag;
using MulLan.Models.User;

namespace LanguageNetwork.Models.Question
{
    public class Question
    {
        public int QuestionID { get; set; }

        public string QuestionTitle { get; set; }
        public string QuestionBody { get; set; }
        public DateTime? QuestionTime { get; set; }
        public int Vote { get; set; }
        public int answer { get; set; }
        public int view { get; set; }
        public List<Tag.Tag> listTag { get; set; }
        public User User { get; set; }
    }
}
