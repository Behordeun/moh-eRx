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
namespace Health.PharmaNet.Authorization.Requirements
{
    using System;
    using System.Text.Json.Serialization;

    using Health.PharmaNet.Authorization.Requirements.Models;

    /// <summary>
    /// The configuration settings for determining Hl7v2 messages permitted for a given scope(s).
    /// </summary>
    public class Hl7v2AuthorizationConfiguration
    {
        /// <summary>
        /// Gets or sets the Hl7v2 Hl7v2Authorization configurations.
        /// </summary>
        ///
        [JsonPropertyName("Hl7v2Authorization")]
        public Hl7v2Authorization Hl7v2Authorization { get; set; } = new Hl7v2Authorization();
    }
}