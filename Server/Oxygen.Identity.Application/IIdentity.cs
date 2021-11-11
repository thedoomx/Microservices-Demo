namespace Oxygen.Identity.Application
{
    using System.Threading.Tasks;
    using Commands;
    using Commands.ChangePassword;
    using Commands.LoginUser;
    using Oxygen.Application.Common;
    using Oxygen.Identity.Application.Commands.CreateUser;

    public interface IIdentity
    {
        Task<Result<IUser>> Register(CreateUserInputModel userInput);

        Task<Result<LoginSuccessModel>> Login(UserInputModel userInput);

        Task<Result> ChangePassword(ChangePasswordInputModel changePasswordInput);
    }
}
