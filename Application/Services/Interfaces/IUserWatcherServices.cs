using System;
using System.Threading.Tasks;
using Application.UserWatchers;
using Domain;

namespace Application.Services.Interfaces
{
    public interface IUserWatcherServices : IBaseServices<UserWatcherDto, UserWatcher>
    {
        Task<UserWatcherDto> GetUserWatcher(Guid courseId, Guid userId);
    }
}