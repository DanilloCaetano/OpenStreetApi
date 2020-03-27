using Newtonsoft.Json;

namespace OpenStreetMapApi.Model
{
    public class ResponseInfos
    {
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Error { get; set; }
    }      
}
