﻿namespace Oxygen.Identity.Application.Commands.ChangePassword
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Oxygen.Application.Common;
    using Oxygen.Application.Common.Contracts;

    public class ChangePasswordCommand : IRequest<Result>
    {
        public string CurrentPassword { get; set; } = default!;

        public string NewPassword { get; set; } = default!;

        public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result>
        {
            private readonly ICurrentUser currentUser;
            private readonly IIdentity identity;

            public ChangePasswordCommandHandler(
                ICurrentUser currentUser,
                IIdentity identity)
            {
                this.currentUser = currentUser;
                this.identity = identity;
            }

            public async Task<Result> Handle(
                ChangePasswordCommand request,
                CancellationToken cancellationToken)
                => await this.identity.ChangePassword(new ChangePasswordInputModel(
                    this.currentUser.UserId,
                    request.CurrentPassword,
                    request.NewPassword));
        }
    }
}
