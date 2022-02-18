namespace Oxygen.Identity.Application.Commands.LoginUser
{
    public class LoginOutputModel
    {
        public LoginOutputModel(string userId, string token)
        {
            this.UserId = userId;
            this.Token = token;
        }

		public string UserId { get; set; }

		public string Token { get; }
    }
}
