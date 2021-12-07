namespace Oxygen.Survey.Domain.Models
{
    using Oxygen.Domain.Common;
    using Oxygen.Domain.Common.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class UserSurvey : Entity<int>, IAggregateRoot
    {
        private readonly HashSet<UserSurveyItem> userSurveyItems;

        internal UserSurvey(string userId, Survey survey, bool isSubmitted)
        {
            this.UserId = userId;
            this.Survey = survey;
            this.IsSubmitted = isSubmitted;

            this.userSurveyItems = new HashSet<UserSurveyItem>();
        }

        private UserSurvey(string userId, bool isSubmitted)
        {
            this.UserId = userId;
            this.IsSubmitted = isSubmitted;

            this.userSurveyItems = new HashSet<UserSurveyItem>();
        }

        public string UserId { get; private set; }

        public Survey Survey { get; private set; }

        public bool IsSubmitted { get; private set; }

        //public UserSurvey SubmitSurvey()
        //{
        //    this.IsSubmitted = !this.IsSubmitted;

        //    return this;
        //}

        public IReadOnlyCollection<UserSurveyItem> UserSurveyItems => 
            this.userSurveyItems.ToList().AsReadOnly();

        public void AddUserSurveyItem(UserSurveyItem userSurveyItem)
        {
            this.userSurveyItems.Add(userSurveyItem);
        }
    }
}
