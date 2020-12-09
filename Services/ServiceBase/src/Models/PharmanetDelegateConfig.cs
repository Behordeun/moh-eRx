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
namespace Health.PharmaNet.Models
{
    /// <summary>
    /// The configuration settings for connecting to the PharmanetProxy.
    /// </summary>
    public class PharmanetDelegateConfig
    {
        /// <summary>
        /// The appsettings.json section.
        /// </summary>
        public const string ConfigurationSectionKey = "PharmanetProxy";

        /// <summary>
        /// Gets or sets the endpoint path for the protective word service.
        /// </summary>
        public string Endpoint { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the username to use for authentication.
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the password to use for authentication.
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the path to the client certificate (pfx) file to use for client authentication.
        /// </summary>
        public string ClientCertificatePath { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the path to the client certificate password/secret (for the pfx file) to use for client authentication.
        /// </summary>
        public string ClientCertificatePassword { get; set; } = string.Empty;
    }
}