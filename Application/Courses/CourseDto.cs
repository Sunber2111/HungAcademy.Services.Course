using System;

namespace Application.Courses
{
    public class CourseDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public string Photo { get; set; }

        public double Price { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsSale { get; set; }

        public float Percent { get; set; }

        public string PhotoId { get; set; }

        public string ConditionRequirement { get; set; }

        public string MainContent { get; set; }

        public string CustomerSegment { get; set; }
    }
}
