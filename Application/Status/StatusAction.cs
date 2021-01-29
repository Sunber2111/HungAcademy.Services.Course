namespace Application.Status
{
    public abstract class StatusAction
    {
        public string Message { get; set; }

        public int Status { get; set; }

        public StatusAction(int status )
        {
            this.Status = status;
        }

    }
}