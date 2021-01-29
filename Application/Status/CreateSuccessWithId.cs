using System;

namespace Application.Status
{
    public class CreateSuccessWithId : StatusAction
    {
        public Guid Id { get; set; }

        public CreateSuccessWithId(Guid id, string entity = null):base(200)
        {
            this.Message = entity != null ? $"Tạo {entity} thành công" : "Tạo thành công";
            this.Id = id;
        }

    }
}