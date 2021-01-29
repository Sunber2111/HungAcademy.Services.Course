using System;
using System.Collections.Generic;

namespace Domain
{
    public class Course
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public string Photo { get; set; }

        public double Price { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsSale { get; set; }

        public float Percent { get; set; }

        public string ConditionRequirement { get; set; }

        public string MainContent { get; set; }

        public string CustomerSegment { get; set; }

        public string PhotoId { get; set; }

        public Guid? CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Section> Sections { get; set; }

        public virtual ICollection<CaseStudy> CaseStudies { get; set; }

    }
}
