﻿using Lesson1.DAL.Interfaces.Base;

namespace Lesson1.DAL.Models
{
    public class Note : BaseEntity
    {
        public string Content { get; set; } = null!;
    }
}
