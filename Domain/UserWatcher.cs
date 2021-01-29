using System;

namespace Domain
{
    public class UserWatcher
    {
        public Guid UserId { get; set; }

        public Guid LectureId { get; set; }

        public virtual Lecture Lecture { get; set; }
    }
}