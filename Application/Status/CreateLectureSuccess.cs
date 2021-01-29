using System;

namespace Application.Status
{
    public class CreateLectureSuccess : StatusAction
    {
        public Guid Id { get; set; }

        public string Video { get; set; }

        public CreateLectureSuccess(Guid id, string video, string entity = null) : base(200)
        {
            this.Message = entity != null ? $"Tạo {entity} thành công" : "Tạo thành công";
            this.Id = id;
            this.Video = video;
        }
    }
}
