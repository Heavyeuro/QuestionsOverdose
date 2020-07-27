using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace QuestionOverdose.Domain.Entities
{
    public class Tag
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string TagName { get; set; }

        [Required]
        public DateTime DateOfCreation { get; set; }

        public virtual List<UserTag> SubscribedUsers { get; set; }

        public virtual List<QuestionTag> QuestionTags { get; set; }
    }
}