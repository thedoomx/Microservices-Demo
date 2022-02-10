namespace Oxygen.Survey.Infrastructure
{
    using Microsoft.EntityFrameworkCore;
    using Oxygen.Infrastructure.Common.Persistence;
    using Oxygen.Survey.Domain.Models;
    using Oxygen.Survey.Infrastructure.Persistence.Models;

    internal interface ISurveyDbContext : IDbContext
    {
        DbSet<Question> Questions { get; }

        DbSet<QuestionItem> QuestionItems { get; }

        DbSet<QuestionType> QuestionTypes { get; }

        DbSet<Domain.Models.Survey> Surveys { get; }

        DbSet<SurveyType> SurveyTypes { get; }

        DbSet<EmployeeSurvey> EmployeeSurveys { get; }

        DbSet<EmployeeSurveyItem> EmployeeSurveyItems { get; }
    }
}
