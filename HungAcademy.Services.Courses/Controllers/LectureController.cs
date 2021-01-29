using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Lectures;
using Application.Status;
using Microsoft.AspNetCore.Mvc;

namespace HungAcademy.Services.Courses.Controllers
{
    public class LectureController : BaseController
    {

        [HttpPost]
        public async Task<StatusAction> UpdateImage([FromForm] Create.Command command) => await Mediator.Send(command);

        [HttpGet]
        public async Task<IEnumerable<LectureDto>> List() => await Mediator.Send(new List.Query());

        [HttpPost]
        public async Task<StatusAction> CreateCourse(Create.Command command) => await Mediator.Send(command);

        [HttpDelete("{id}")]
        public async Task<StatusAction> Delete(Guid id) => await Mediator.Send(new Delete.Command { Id = id });

        [HttpPut]
        public async Task<StatusAction> Update(Update.Command command) => await Mediator.Send(command);

        
    }

}

