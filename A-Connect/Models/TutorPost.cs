﻿using System;
using SQLite;

namespace A_Connect.Models
{
    public class TutorPost
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string CourseCode { get; set; }

        // e.g., "Tutor" or "Tutee"
        public string Category { get; set; }

        // Poster’s name (now automatically assigned from the logged‐in user)
        public string PosterName { get; set; }

        public string PosterContact { get; set; }

        public string AdditionalInfo { get; set; }

        // New: Date the post was created
        public DateTime Date { get; set; }
        public bool IsOwnPost { get; set; }
    }
}
