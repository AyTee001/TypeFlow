using TypeFlow.Application.Services.User.Dto;

namespace TypeFlow.Application.Services.User
{
    public interface IUserService
    {
        Task<FullUserData> GetFullUserData(Guid userId);
    }
}
