//-------------------------------------------------------------------------
// Copyright © 2020 Province of British Columbia
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
namespace Health.PharmaNet.Services
{
    using System.Threading.Tasks;

    using Health.PharmaNet.Delegates;
    using Health.PharmaNet.Models;

    using Hl7.Fhir.Model;

    using Microsoft.Extensions.Logging;

    /// <summary>
    /// The PharmanetService.
    /// </summary>
    public class PharmanetService : IPharmanetService
    {
        private readonly IPharmanetDelegate pharmanetDelegate;
        private readonly ILogger<PharmanetService> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PharmanetService"/> class.
        /// </summary>
        /// <param name="logger">Injected Logger Provider.</param>
        /// <param name="pharmanetDelegate">Injected delegate.</param>
        public PharmanetService(
            ILogger<PharmanetService> logger,
            IPharmanetDelegate pharmanetDelegate)
        {
            this.pharmanetDelegate = pharmanetDelegate;
            this.logger = logger;
        }

        /// <summary>
        /// Submit Request to Pharmanet.
        /// </summary>
        /// <param name="request">The DocumentReference to be submitted.</param>
        /// <returns>Returns a DocumentReference containing the response from PharmaNet.</returns>
        public async Task<DocumentReference> SubmitRequest(DocumentReference request)
        {
            PharmanetDelegateMessageModel requestMessage = PharmanetDelegateAdapter.FromDocumentReference(request);
            PharmanetDelegateMessageModel responseMessage = await this.pharmanetDelegate.SubmitRequest(requestMessage).ConfigureAwait(false);

            ResourceReference reference = PharmanetDelegateAdapter.RelatedToDocumentReference(request);
            DocumentReference response = PharmanetDelegateAdapter.FromPharmanetProxyMessage(responseMessage, reference);
            return response;
        }
    }
}
