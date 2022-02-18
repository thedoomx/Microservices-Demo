﻿namespace Oxygen.Identity.Application.Commands.LoginUser
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Oxygen.Application.Common;

    public class LoginUserCommand : UserInputModel, IRequest<Result<LoginOutputModel>>
    {
        public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<LoginOutputModel>>
        {
            private readonly IIdentity identity;

            public LoginUserCommandHandler(
                IIdentity identity)
            {
                this.identity = identity;
            }

            public async Task<Result<LoginOutputModel>> Handle(
                LoginUserCommand request,
                CancellationToken cancellationToken)
            {
                var result = await this.identity.Login(request);

                if (!result.Succeeded)
                {
                    return result.Errors;
                }

                var user = result.Data;


                return new LoginOutputModel(user.UserId, user.Token);
            }
        }
    }
}
