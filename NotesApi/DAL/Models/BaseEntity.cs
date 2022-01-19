﻿using NotesApi.DAL.Interfaces.Base;

namespace Lesson1.DAL.Models
{
    public class BaseEntity : IEntity
    {
        public int Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.Now;
    }
}