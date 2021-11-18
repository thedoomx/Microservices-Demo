namespace Oxygen.Survey.Application.Survey.Commands.Create
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Common;
    using MediatR;
    using Oxygen.Application.Common;
    using Oxygen.Application.Common.Services.Identity;
    using Oxygen.Survey.Domain.Factories;
    using Oxygen.Survey.Domain.Repositories;

    public class CreateUsersSurveysCommand : EntityCommand<int>, IRequest<Result>
    {
        IEnumerable<string> UserIds { get; set; } = default!;

        public class CreateUsersSurveysCommandHandler : IRequestHandler<CreateUsersSurveysCommand, Result>
        {
            private readonly ICurrentUserService _currentUserService;
            private readonly ISurveyDomainRepository _surveyRepository;
            private readonly IUserSurveyDomainRepository _userSurveyRepository;
            private readonly IUserSurveyFactory _userSurveyFactory;

            public CreateUsersSurveysCommandHandler(
                ICurrentUserService currentUserService,
                ISurveyDomainRepository surveyRepository,
                IUserSurveyDomainRepository userSurveyRepository,
                IUserSurveyFactory userSurveyFactory)
            {
                this._currentUserService = currentUserService;
                this._surveyRepository = surveyRepository;
                this._userSurveyRepository = userSurveyRepository;
                this._userSurveyFactory = userSurveyFactory;
            }

            public async Task<Result> Handle(
                CreateUsersSurveysCommand request,
                CancellationToken cancellationToken)
            {
                var survey = await this._surveyRepository.GetById(request.Id,
                    cancellationToken);

                if (survey == null)
                {
                    throw new KeyNotFoundException();
                }

                foreach (var userId in request.UserIds)
                {
                    var userSurvey = this._userSurveyFactory
                    .WithUserId(userId)
                    .WithSurveyId(request.Id)
                    .Build();

                    await this._userSurveyRepository.Save(userSurvey, cancellationToken);
                }

                return Result.Success;
            }
        }
    }

}
