using OpenStreetMapApi.Model;
using System.Threading.Tasks;

namespace OpenStreetMapApi.Services.Interfaces
{
    public interface IOpenStreetService
    {
        Task<ResponseInfos> GetInfosByLatLon(string lat, string lon);
    }
}
