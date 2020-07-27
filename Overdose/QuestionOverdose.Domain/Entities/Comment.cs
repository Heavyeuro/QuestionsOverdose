using System;
using System.ComponentModel.DataAnnotations;

namespace QuestionOverdose.Domain.Entities
{
    public class Comment
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public DateTime DateOfCreation { get; set; }

        public int AuthorId { get; set; }

        [Required]
        public virtual User Author { get; set; }

        public int AnswerId { get; set; }

        public virtual Answer Answer { get; set; }

        public int? CommentAncestorId { get; set; }

        public virtual Comment CommentAncestor { get; set; }
    }
}