using System;
using System.Collections.Generic;
using System.Text;

namespace QuestionOverdose.DTO
{
    public class TagDTO
    {
        public int Id { get; set; }

        public string TagName { get; set; }

        public DateTime DateOfCreation { get; set; }

        public string Description { get; set; }
    }
}