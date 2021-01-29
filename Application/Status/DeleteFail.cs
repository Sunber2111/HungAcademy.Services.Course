namespace Application.Status
{
    public class DeleteFail:StatusAction
    {
        public DeleteFail(string reason):base(400)
        {
            this.Message = reason;
        }
    }
}
