namespace Application.Status
{
    public class UpdateSuccess : StatusAction
    {
        public UpdateSuccess(string entity = null) : base(200)
        {
            this.Message = entity != null ? $"Cập nhật {entity} thành công" : "Cập nhật thành công";
        }
    }
}