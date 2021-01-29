using System;
using System.Collections.Generic;

namespace Domain
{
    public class Section
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid CourseId { get; set; }

        public virtual Course Course { get; set; }

        public virtual ICollection<Lecture> Lectures { get; set; }
    }
}
