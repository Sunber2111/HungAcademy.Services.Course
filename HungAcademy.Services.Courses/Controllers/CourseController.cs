using Application.Courses;
using Application.Status;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HungAcademy.Services.Courses.Controllers
{
    public class CourseController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<List<CourseDto>>> List() => await Mediator.Send(new List.Query());

        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDto>> Detail(Guid id) => await Mediator.Send(new Detail.Query { Id = id });

        [HttpPost]
        public async Task<StatusAction> CreateCourse(Create.Command command) => await Mediator.Send(command);

        [HttpDelete("{id}")]
        public async Task<StatusAction> Delete(Guid id) => await Mediator.Send(new Delete.Command { Id = id });

        [HttpPut]
        public async Task<StatusAction> Update(Update.Command command) => await Mediator.Send(command);

        [HttpPut("updatecategory/{id}/{cateId}")]
        public async Task<StatusAction> UpdateCategory(Guid id, Guid cateId) => await Mediator.Send(new UpdateCategory.Command { CategoryId = cateId, CourseId = id });

        [HttpGet("category/{id}")]
        public async Task<List<CourseDto>> ListByCategoryId(Guid id) => await Mediator.Send(new ListByCategoryId.Query { CategoryId = id });

        [HttpPut("image/{id}")]
        public async Task<StatusAction> UpdateImage(Guid Id, [FromForm] IFormFile File) => await Mediator.Send(new CreateImage.Command { CourseId = Id, File = File });

    }
}
