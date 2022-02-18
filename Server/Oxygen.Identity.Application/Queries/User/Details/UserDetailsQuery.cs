namespace Oxygen.Identity.Application.Queries.User.Details
{
	using System.Threading;
    using System.Threading.Tasks;
    using MediatR;

    public class UserDetailsQuery : IRequest<UserDetailsOutputModel>
    {
		public string Id { get; set; }

		public class UserDetailsQueryQueryHandler : IRequestHandler<UserDetailsQuery, UserDetailsOutputModel>
        {
            private readonly IIdentity _userService;

            public UserDetailsQueryQueryHandler(IIdentity userService)
                => this._userService = userService;

            public async Task<UserDetailsOutputModel> Handle(
                UserDetailsQuery request,
                CancellationToken cancellationToken)
                => await this._userService.GetDetails(request.Id, cancellationToken);
        }
    }
}
