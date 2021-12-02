﻿using Microsoft.AspNetCore.Mvc;
using Oxygen.Survey.Application.Queries.Common;
using Oxygen.Survey.Application.Queries.Mine;
using Oxygen.Survey.Application.Survey.Commands.Create;
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
        [Route(nameof(Test))]
        public async Task<ActionResult<int>> Test()
        {
            var a = new CreateSurveyCommand();
            a.Name = "Test";
            a.Summary = "Summary";
            a.SurveyType = 1;

            return 5;
        }

        [HttpGet]
        public async Task<ActionResult<CreateSurveyOutputModel>> Create(
            [FromQuery] CreateSurveyCommand command)
            => await this.Send(command);

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SurveyOutputModel>>> Search(
            [FromQuery] MineSurveysQuery query)
            => await this.Send(query);
    }
}
