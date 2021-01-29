namespace Application.Status
{
    public class CreateSuccess : StatusAction
    {
        public CreateSuccess(string entity = null) : base(200)
        {
            this.Message = entity != null ? $"Tạo {entity} thành công" : "Tạo thành công";
        }
    }
}