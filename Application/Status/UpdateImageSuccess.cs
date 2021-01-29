using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Status
{
    public class UpdateImageSuccess : StatusAction
    {
        public string Photo { get; set; }

        public UpdateImageSuccess():base(200)
        {
            this.Message = "Cập nhật hình ảnh thành công";
        }

    }
}
