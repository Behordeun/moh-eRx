﻿//-------------------------------------------------------------------------
// Copyright © 2019 Province of British Columbia
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//-------------------------------------------------------------------------
namespace Health.PharmaNet.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Security.Claims;


    using Health.PharmaNet.Common.Authorization.Policy;
    using Health.PharmaNet.Models;
    using Health.PharmaNet.Services;
    using Hl7.Fhir.Model;
    using Hl7.Fhir.Serialization;


    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// The Template controller.
    /// </summary>
    [ApiVersion("1.0")]
    //[Route("/api/v{version:apiVersion}/[controller]/")]
    [Route("/api/v{version:apiVersion}/MedicationService/")]
    [ApiController]
    public class ServiceBaseController : ControllerBase
    {
        private readonly ILogger logger;

        /// <summary>
        /// Gets or sets the MedicationRequest Service
        /// </summary>
        private readonly IPharmanetService service;

        /// <summary>
        /// Gets or sets the http context accessor.
        /// </summary>
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceBaseController"/> class.
        /// </summary>
        /// <param name="logger">Injected Logger Provider.</param>
        /// <param name="service">Injected service.</param>
        /// <param name="httpContextAccessor">The Http Context accessor.</param>
        public ServiceBaseController(
            ILogger<ServiceBaseController> logger,
            IPharmanetService service,
            IHttpContextAccessor httpContextAccessor)
        {
            this.service = service;
            this.logger = logger;
            this.httpContextAccessor = httpContextAccessor;
        }

        private DocumentReference parseJsonBody(string json)
        {
            FhirJsonParser parser = new FhirJsonParser( new ParserSettings { AcceptUnknownMembers = true, AllowUnrecognizedEnums = true});
            DocumentReference request = parser.Parse<DocumentReference>(json);
            return request;
        }

        /// <summary>
        /// Healthcheck API implementation.
        /// </summary>
        /// <returns>JSon status = available.</returns>
        /// <response code="200">Always returns Ok and HTTP Response code of 200.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("healthcheck")]
        [Produces("application/json")]
        public ActionResult<string> HealthCheck()
        {
            this.logger.LogDebug($"Healthcheck");
            // TODO: check ODR delegate is alive?
            return "{'status' : 'available'}";
        }

        /// <summary> 
        /// Takes HL7 FHIR DocumentReference Object containing HL7v2 payload.
        /// </summary>
        /// <returns>The DocumentReference Response, or error JSON</returns>
        /// <response code="200">Returns Ok when the transaction went through</response>
        /// <response code="401">Authorization error, returns JSON describing the error</response>
        /// <response code="503">The service is unavailable for use.</response>
        [HttpPost]
        [Produces("application/fhir+json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("MedicationRequest")]
        [Authorize(Policy = FhirScopesPolicy.Access)]
        public async Task<ActionResult<JsonResult>> PharmanetRequest([FromBody] string json)
        {
            ClaimsPrincipal user = this.httpContextAccessor.HttpContext.User;
            string accessToken = await this.httpContextAccessor.HttpContext.GetTokenAsync("access_token").ConfigureAwait(true);

            DocumentReference request = this.parseJsonBody(json);
            DocumentReference response = await this.service.SubmitRequest(request);

            return new JsonResult(response.ToJson());
        }
    }
}
