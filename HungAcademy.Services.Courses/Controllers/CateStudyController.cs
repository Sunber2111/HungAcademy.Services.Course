using System;
using Microsoft.AspNetCore.Mvc;
using Application.CaseStudies;
using System.Threading.Tasks;
using System.Collections.Generic;
using Application.Status;

namespace HungAcademy.Services.Courses.Controllers
{
    public class CateStudyController : BaseController
    {
        [HttpGet("course/{courseId}")]
        public async Task<List<CaseStudyDto>> GetByCourseId(Guid courseId) => await Mediator.Send(new ListByCourseId.Command() { CourseId = courseId });

        [HttpPost]
        public async Task<StatusAction> Create(Create.Command command) => await Mediator.Send(command);

        [HttpPut]
        public async Task<StatusAction> Update(Update.Command command) => await Mediator.Send(command);

        [HttpDelete("{id}")]
        public async Task<StatusAction> Delete(Guid id) => await Mediator.Send(new Delete.Command { Id = id });
    }
}
