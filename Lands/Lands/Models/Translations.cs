namespace Lands.Models
{
    using Newtonsoft.Json;

    public class Translations
    {
        [JsonProperty("de")]
        public string Aleman { get; set; }

        [JsonProperty("es")]
        public string Espanol { get; set; }

        [JsonProperty("fr")]
        public string Frances { get; set; }

        [JsonProperty("ja")]
        public string Japones { get; set; }

        [JsonProperty("it")]
        public string Italiano { get; set; }

        [JsonProperty("br")]
        public string Brasileno { get; set; }

        [JsonProperty("pt")]
        public string Portugues { get; set; }

        [JsonProperty("nl")]
        public string Holandes { get; set; }

        [JsonProperty("hr")]
        public string Croata { get; set; }

        [JsonProperty("fa")]
        public string Danes { get; set; }
    }
}
