namespace Oxygen.Identity.Application
{
    using System.Threading.Tasks;
    using Commands;
    using Commands.ChangePassword;
    using Commands.LoginUser;
    using Oxygen.Application.Common;
    using Oxygen.Identity.Application.Commands.CreateUser;
    using Oxygen.Identity.Application.Queries.User.Details;
    using System.Threading;

    public interface IIdentity
    {
        Task<Result<IUser>> Register(CreateUserInputModel userInput);

        Task<Result<LoginSuccessModel>> Login(UserInputModel userInput);

        Task<Result> ChangePassword(ChangePasswordInputModel changePasswordInput);

        Task<UserDetailsOutputModel> GetDetails(string userId, CancellationToken cancellationToken);
    }
}
