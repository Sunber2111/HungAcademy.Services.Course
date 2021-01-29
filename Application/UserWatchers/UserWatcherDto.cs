using System;
using System.Collections.Generic;

namespace Application.UserWatchers
{
    public class UserWatcherDto
    {
        public ICollection<SectionCourseDto> SectionsWatcher { get; set; }

        public UserWatcherDto()
        {
            SectionsWatcher = new List<SectionCourseDto>();
        }
    }

    public class SectionCourseDto
    {
        public Guid Id { get; set; }
        public bool IsWatchSection { get; set; }
        public ICollection<LectureCourseDto> LecturesWatcher { get; set; }

        public SectionCourseDto()
        {
            LecturesWatcher = new List<LectureCourseDto>();
        }
    }

    public class LectureCourseDto
    {
        public Guid Id { get; set; }
        public bool IsWatchLecture { get; set; }
    }
}