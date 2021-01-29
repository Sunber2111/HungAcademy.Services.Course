using System;
using System.Collections.Generic;

namespace Domain
{
    public class Lecture
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Video { get; set; }

        public string VideoId { get; set; }

        public string VideoTime { get; set; }

        public Guid SectionId { get; set; }

        public virtual Section Section { get; set; }

        public virtual ICollection<UserWatcher> UserWatchers { get; set; }
    }
}
