namespace Oxygen.Company.Gateway.Services.Company
{
    using System.Threading.Tasks;
	using Oxygen.Company.Gateway.Models.Company;
	using Refit;

    public interface ICompanyService
    {
        [Get("/Company/{Id}")]
        Task<EmployeeOutputModel> Get(int Id);
    }
}
