using System;

namespace QuestionOverdose.DTO
{
    public class CommentDTO
    {
        public int Id { get; set; }

        public string Body { get; set; }

        public DateTime DateOfCreation { get; set; }

        public virtual UserDTO Author { get; set; }

        public virtual AnswerDTO Answer { get; set; }

        public virtual CommentDTO CommentAncestor { get; set; }

        public int? CommentAncestorId { get; set; }
    }
}