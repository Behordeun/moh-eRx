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
namespace PharmaNet.Client.Models
{
    public partial class OidcConfiguration
    {
        public string issuer { get; set; }
        public string authorization_endpoint { get; set; }

        public string token_endpoint { get; set; }

        public string token_introspection_endpoint { get; set; }

        public string userinfo_endpoint { get; set; }

        public string jwks_uri { get; set; }
    }
}