using Newtonsoft.Json;

namespace DataManager
{
    public class CatalogRecord
    {
        public int _index { get; set; }
        public string _session_id { get; set; }
        public long _timestamp_ms { get; set; }

        [JsonProperty("cam/image_array")]
        public string ImageArray { get; set; }

        [JsonProperty("user/angle")]
        public double Angle { get; set; }

        [JsonProperty("user/throttle")]
        public double Throttle { get; set; }

        [JsonProperty("user/mode")]
        public string Mode { get; set; }
    }
}