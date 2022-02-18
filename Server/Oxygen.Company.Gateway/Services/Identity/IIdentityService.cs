namespace Oxygen.Company.Gateway.Services.Identity
{
	using System.Threading.Tasks;
	using Oxygen.Company.Gateway.Models.Identity;
	using Refit;

    public interface IIdentityService
    {
        [Get("/Identity/GetDetails?Id={Id}")]
        Task<UserDetailsOutputModel> GetDetails([AliasAs("Id")] string Id);
    }
}
