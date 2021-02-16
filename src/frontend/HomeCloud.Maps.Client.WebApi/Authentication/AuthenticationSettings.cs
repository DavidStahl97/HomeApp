using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Client.WebApi.Authentication
{
    public class AuthenticationSettings
    {
        [JsonPropertyName("authority")]
        public string Authority { get; set; }

        [JsonPropertyName("metadataUrl")]
        public string Metadata { get; set; }

        [JsonPropertyName("client_id")]
        public string ClientId { get; set; }

        [JsonPropertyName("defaultScopes")]
        public string[] DefaultScopes { get; set; }

        [JsonPropertyName("redirect_uri")]
        public string RedirectUri { get; set; }

        [JsonPropertyName("post_logout_redirect_uri")]
        public string PostLogoutRedirectUri { get; set; }

        [JsonPropertyName("response_type")]
        public string ResponseType { get; set; }

        [JsonPropertyName("response_mode")]
        public string ResponseMode { get; set; }

        [JsonPropertyName("scope")]
        public string Scope { get; set; }

        public string OIDCUserKey => $"oidc.user:{Authority}:{ClientId}";
    }
}
