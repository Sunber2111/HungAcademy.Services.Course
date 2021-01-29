using System;

namespace Application.Lectures
{
    public class LectureDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Video { get; set; }

        public string VideoId { get; set; }

        public double VideoTime { get; set; }

    }
}