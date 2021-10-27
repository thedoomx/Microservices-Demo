namespace Oxygen.Survey.Domain.Models
{
    using Oxygen.Domain.Common;
    using Oxygen.Domain.Common.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    //ToDo: pending state of survey
    public class UserSurvey : Entity<int>, IAggregateRoot
    {
        private readonly HashSet<UserSurveyItem> userSurveyItems;

        internal UserSurvey(string userId, int surveyId, bool isSubmitted)
        {
            //this.Validate(description);

            this.UserId = userId;
            this.SurveyId = surveyId;
            this.IsSubmitted = isSubmitted;

            this.userSurveyItems = new HashSet<UserSurveyItem>();
        }

        public string UserId { get; private set; }

        public int SurveyId { get; private set; }

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

            //this.RaiseEvent(new userSurveyItemAddedEvent());
        }

        //private void Validate(string description)
        //{
        //    this.ValidateType(description);
        //}

        //private void ValidateType(string description)
        //    => Guard.ForStringLength<InvalidQuestionItemException>(
        //        description,
        //        MinDescriptionLength,
        //        MaxDescriptionLength,
        //        nameof(this.Description));
    }
}
