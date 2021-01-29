using Application.Categories;
using Application.Status;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HungAcademy.Services.Courses.Controllers
{
    public class CategoryController : BaseController
    {
        [HttpGet]
        public async Task<IEnumerable<CategoryDto>> List() => await Mediator.Send(new List.Query());

        [HttpPost]
        public async Task<StatusAction> InsertCategory(Create.Command command) => await Mediator.Send(command);

        [HttpPut]
        public async Task<StatusAction> UpdateCategory(Update.Command command) => await Mediator.Send(command);

        [HttpDelete("{id}")]
        public async Task<StatusAction> DeleteCategory(Guid id) => await Mediator.Send(new Delete.Command { Id = id });
    }
}
