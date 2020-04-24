namespace Lands.Models
{
    using System;
    using Newtonsoft.Json;

    public class TokenResponse
    {
        #region Propiedades
        [JsonProperty("acces_Token")]
        public string AccesToken { get; set; }

        [JsonProperty("acces_Token")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("user_Name")]
        public string UserName { get; set; }

        [JsonProperty(".issued")]
        public DateTime Issued { get; set; }

        [JsonProperty(".expires")]
        public DateTime Expires { get; set; }

        [JsonProperty("error_description")]
        public string ErrorDescription { get; set; }
        #endregion
    }
}
