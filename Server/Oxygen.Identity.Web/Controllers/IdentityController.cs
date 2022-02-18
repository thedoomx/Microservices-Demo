namespace Oxygen.Identity.Web.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Oxygen.Identity.Application.Commands.CreateUser;
    using Oxygen.Identity.Application.Commands.ChangePassword;
    using Oxygen.Identity.Application.Commands.LoginUser;
    using Oxygen.Web.Common;
	using Oxygen.Identity.Application.Queries.User.Details;

	public class IdentityController : ApiController
    {
        [HttpPost]
        [Route(nameof(Register))]
        public async Task<ActionResult> Register(
            CreateUserCommand command)
            => await this.Send(command);

        [HttpPost]
        [Route(nameof(Login))]
        public async Task<ActionResult<LoginOutputModel>> Login(
            LoginUserCommand command)
            => await this.Send(command);

        [HttpPut]
        [Authorize]
        [Route(nameof(ChangePassword))]
        public async Task<ActionResult> ChangePassword(
            ChangePasswordCommand command)
            => await this.Send(command);

        [HttpGet]
        [Route(nameof(GetDetails))]
        public async Task<ActionResult<UserDetailsOutputModel>> GetDetails(
            [FromQuery] UserDetailsQuery query)
            => await this.Send(query);
    }
}
