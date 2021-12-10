namespace Oxygen.Identity.Application.Commands.CreateUser
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Oxygen.Application.Common;

    public class CreateUserCommand : CreateUserInputModel, IRequest<Result>
    {
        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result>
        {
            private readonly IIdentity identity;

            public CreateUserCommandHandler(
                IIdentity identity)
            {
                this.identity = identity;
            }

            public async Task<Result> Handle(
                CreateUserCommand request,
                CancellationToken cancellationToken)
            {
                var result = await this.identity.Register(request);

                if (!result.Succeeded)
                {
                    return result;
                }

                var user = result.Data;

                return result;
            }
        }
    }
}
