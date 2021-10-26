using Microsoft.AspNetCore.Mvc;
using Oxygen.Survey.Application.Queries.Common;
using Oxygen.Survey.Application.Queries.Mine;
using Oxygen.Web.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Oxygen.Survey.Web.Controllers
{
    public class SurveyController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SurveyOutputModel>>> Search(
            [FromQuery] MineSurveysQuery query)
            => await this.Send(query);
    }
}
