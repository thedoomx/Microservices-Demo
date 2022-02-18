namespace Oxygen.Identity.Infrastructure
{
    using System.Linq;
	using System.Threading;
	using System.Threading.Tasks;
	using AutoMapper;
	using Microsoft.AspNetCore.Identity;
    using Oxygen.Application.Common;
    using Oxygen.Identity.Application;
    using Oxygen.Identity.Application.Commands;
    using Oxygen.Identity.Application.Commands.ChangePassword;
    using Oxygen.Identity.Application.Commands.CreateUser;
    using Oxygen.Identity.Application.Commands.LoginUser;
	using Oxygen.Identity.Application.Queries.User.Details;

	internal class IdentityService : IIdentity
    {
        private const string InvalidErrorMessage = "Invalid credentials.";

        private readonly UserManager<User> userManager;
        private readonly IJwtTokenGenerator jwtTokenGenerator;
        private readonly IMapper mapper;


        public IdentityService(UserManager<User> userManager, IJwtTokenGenerator jwtTokenGenerator, IMapper mapper)
        {
            this.userManager = userManager;
            this.jwtTokenGenerator = jwtTokenGenerator;
            this.mapper = mapper;
        }

        public async Task<Result<IUser>> Register(CreateUserInputModel userInput)
        {
            var user = new User(
                userInput.Email,
                userInput.FirstName,
                userInput.SurName,
                userInput.LastName,
                userInput.Department,
                userInput.JobTitle,
                userInput.Office);

            var identityResult = await this.userManager.CreateAsync(user, userInput.Password);

            var errors = identityResult.Errors.Select(e => e.Description);

            return identityResult.Succeeded
                ? Result<IUser>.SuccessWith(user)
                : Result<IUser>.Failure(errors);
        }

        public async Task<Result<LoginSuccessModel>> Login(UserInputModel userInput)
        {
            var user = await this.userManager.FindByEmailAsync(userInput.Email);
            if (user == null)
            {
                return InvalidErrorMessage;
            }

            var passwordValid = await this.userManager.CheckPasswordAsync(user, userInput.Password);
            if (!passwordValid)
            {
                return InvalidErrorMessage;
            }

            var token = this.jwtTokenGenerator.GenerateToken(user);

            return new LoginSuccessModel(user.Id, token);
        }

        public async Task<Result> ChangePassword(ChangePasswordInputModel changePasswordInput)
        {
            var user = await this.userManager.FindByIdAsync(changePasswordInput.UserId);

            if (user == null)
            {
                return InvalidErrorMessage;
            }

            var identityResult = await this.userManager.ChangePasswordAsync(
                user,
                changePasswordInput.CurrentPassword,
                changePasswordInput.NewPassword);

            var errors = identityResult.Errors.Select(e => e.Description);

            return identityResult.Succeeded
                ? Result.Success
                : Result.Failure(errors);
        }

        public async Task<UserDetailsOutputModel> GetDetails(string userId, CancellationToken cancellationToken)
		{
            var user = await this.userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return null;
            }

            var result = new UserDetailsOutputModel()
            {
                Email = user.Email,
                UserName = user.UserName,
            };

            return result;
        }

    }
}
