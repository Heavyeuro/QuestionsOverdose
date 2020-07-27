using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionOverdose.Models.QuestionModels
{
    public class CommentModel
    {
        public int Id { get; set; }

        public string Body { get; set; }

        public DateTime DateOfCreation { get; set; }

        public string AuthorName { get; set; }

        public List<CommentModel> CommentChilds { get; set; }

        public int? CommentAncestorId { get; set; }
    }
}
