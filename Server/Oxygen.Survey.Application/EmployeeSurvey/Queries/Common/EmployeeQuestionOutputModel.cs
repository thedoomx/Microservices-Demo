namespace Oxygen.Survey.Application.EmployeeSurvey.Queries.Common
{
    using AutoMapper;
    using Oxygen.Survey.Domain.Models;
    using System.Collections.Generic;

    public class EmployeeQuestionOutputModel
	{
        public int Id { get; private set; }

        public string Description { get; set; } = default!;

        public bool IsRequired { get; set; }

        public string QuestionType { get; set; } = default!;

        public virtual void Mapping(Profile mapper)
            => mapper
                .CreateMap<Question, EmployeeQuestionOutputModel>()
                .ForMember(x => x.QuestionType, cfg => cfg
                    .MapFrom(x => x.QuestionType.Type));
    }
}
