﻿using System;
using System.Collections.Generic;
using System.Text;

namespace QuestionOverdose.Domain.Entities
{
    public class UserTag
    {
        public int UserId { get; set; }

        public User User { get; set; }

        public int TagId { get; set; }

        public Tag Tag { get; set; }
    }
}