namespace Application.Status
{
    public class DeleteSuccess : StatusAction
    {
        public DeleteSuccess(string entity) : base(200)
        {
            this.Message = entity != null ? $"Xóa {entity} thành công" : "Xóa thành công";
        }
    }
}