using System;

namespace Domain
{
    public class CaseStudy
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public Guid CourseId { get; set; }

        public virtual Course Course { get; set; }
    }
}
