namespace Oxygen.Survey.Infrastructure
{
    using Microsoft.EntityFrameworkCore;
    using Oxygen.Infrastructure.Common.Persistence;
    using Oxygen.Survey.Domain.Models;

    internal interface ISurveyDbContext : IDbContext
    {
        DbSet<Question> Questions { get; }

        DbSet<QuestionAnswer> QuestionAnswers { get; }

        DbSet<QuestionType> QuestionTypes { get; }

        DbSet<Survey> Surveys { get; }

        DbSet<SurveyType> SurveyTypes { get; }

        DbSet<EmployeeSurvey> EmployeeSurveys { get; }

        DbSet<EmployeeSurveyAnswer> EmployeeSurveyAnswers { get; }
    }
}
