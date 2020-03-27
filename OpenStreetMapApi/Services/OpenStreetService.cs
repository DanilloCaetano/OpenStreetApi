using Newtonsoft.Json;
using OpenStreetMapApi.Model;
using OpenStreetMapApi.Services.Interfaces;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace OpenStreetMapApi.Services
{
    public class OpenStreetService : IOpenStreetService
    {
        public async Task<ResponseInfos> GetInfosByLatLon(string lat, string lon)
        {
            using (var httpClient = new HttpClient())
            {
                var url = $"https://nominatim.openstreetmap.org/reverse?lat={lat}&lon={lon}";
                httpClient.DefaultRequestHeaders.Add("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                httpClient.DefaultRequestHeaders.Add("sec-fetch-site", "none");
                httpClient.DefaultRequestHeaders.Add("sec-fetch-mode", "navigate");
                httpClient.DefaultRequestHeaders.Add("authority", "nominatim.openstreetmap.org");
                httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36");
                httpClient.DefaultRequestHeaders.Add("sec-fetch-user", "?1");
                httpClient.DefaultRequestHeaders.Add("upgrade-insecure-requests", "1");

                var response = await httpClient.GetAsync(url);

                var strXml = await response.Content.ReadAsStringAsync();

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(strXml);

                var result = BuildResponse(strXml);

                return result;
            }
        }

        private ResponseInfos BuildResponse(string strXml)
        {
            var xml = XElement.Parse(strXml);
            var response = new ResponseInfos();
            var principalEl = xml.Elements("addressparts").FirstOrDefault();
            if (principalEl != null)
            {
                response.City = principalEl.Element("city").Value;
                response.Country = principalEl.Element("country")?.Value ?? "";
                response.PostalCode = principalEl.Element("postcode")?.Value ?? "";
                response.State = principalEl.Element("state").Value;                                
            }
            else
            {
                response.Error = "Houve um erro ao recuperar response Xml :(";
            }

            return response;
        }
    }
}
