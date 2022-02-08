namespace Oxygen.Company.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Oxygen.Company.Application.Department.Queries.Common;
    using Oxygen.Company.Application.Department.Queries.Search;
	using Oxygen.Company.Application.Employee.Queries.Common;
	using Oxygen.Company.Application.Employee.Queries.Search;
	using Oxygen.Company.Application.JobTitle.Queries.Common;
    using Oxygen.Company.Application.JobTitle.Queries.Search;
    using Oxygen.Company.Application.Office.Queries.Common;
    using Oxygen.Company.Application.Office.Queries.Search;
    using Oxygen.Web.Common;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public class CompanyController : ApiController
    {
        [HttpGet]
        [Route(nameof(GetDepartments))]
        public async Task<ActionResult<IEnumerable<DepartmentOutputModel>>> GetDepartments(
            [FromQuery] SearchDepartmentsQuery query)
            => await this.Send(query);

        [HttpGet]
        [Route(nameof(GetJobTitles))]
        public async Task<ActionResult<IEnumerable<JobTitleOutputModel>>> GetJobTitles(
            [FromQuery] SearchJobTitlesQuery query)
            => await this.Send(query);

        [HttpGet]
        [Route(nameof(GetOffices))]
        public async Task<ActionResult<IEnumerable<OfficeOutputModel>>> GetOffices(
            [FromQuery] SearchOfficesQuery query)
            => await this.Send(query);

        [HttpGet]
        [Route(nameof(GetEmployees))]
        public async Task<ActionResult<IEnumerable<EmployeeOutputModel>>> GetEmployees(
            [FromQuery] SearchEmployeesQuery query)
            => await this.Send(query);
    }
}
