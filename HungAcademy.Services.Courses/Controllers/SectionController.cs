using Application.Sections;
using Application.Status;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HungAcademy.Services.Courses.Controllers
{
    public class SectionController:BaseController
    {
        [HttpGet]
        public async Task<IEnumerable<SectionDto>> List() => await Mediator.Send(new List.Query());

        [HttpPost]
        public async Task<StatusAction> CreateCourse(Create.Command command) => await Mediator.Send(command);

        [HttpDelete("{id}")]
        public async Task<StatusAction> Delete(Guid id) => await Mediator.Send(new Delete.Command { Id = id });

        [HttpPut]
        public async Task<StatusAction> Update(Update.Command command) => await Mediator.Send(command);
    }
}
