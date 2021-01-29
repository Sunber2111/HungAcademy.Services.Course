using System.Threading.Tasks;
using Application.Status;
using Application.UserWatchers;
using Microsoft.AspNetCore.Mvc;

namespace HungAcademy.Services.Courses.Controllers
{
    public class UserWatcherController:BaseController
    {
        [HttpGet]
        public async Task<UserWatcherDto> GetAllUserWatcher(List.Command command) => await Mediator.Send(command);

        [HttpPost]
        public async Task<StatusAction> Create(Create.Command command) => await Mediator.Send(command);
    }
}