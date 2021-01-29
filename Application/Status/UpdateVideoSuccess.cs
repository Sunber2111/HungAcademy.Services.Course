namespace Application.Status
{
    public class UpdateVideoSuccess : StatusAction
    {
        public string Video { get; set; }
        public UpdateVideoSuccess(string urlVideo) : base(200)
        {
            this.Message = "Cập nhật video thành công";
            this.Video = urlVideo;
        }
    }
}