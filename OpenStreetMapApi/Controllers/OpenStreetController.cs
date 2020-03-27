using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OpenStreetMapApi.Model;
using OpenStreetMapApi.Services.Interfaces;

namespace OpenStreetMapApi.Controllers
{
    [ApiController]
    public class OpenStreetController : Controller
    {
        private readonly IOpenStreetService _openStreetService;

        public OpenStreetController(IOpenStreetService openStreetService)
        {
            _openStreetService = openStreetService;
        }

        [HttpGet, Route("/getInfos")]
        public async Task<ResponseInfos> GetInfo(string lat, string lon)
        {
            if (string.IsNullOrEmpty(lat) || string.IsNullOrEmpty(lon)) return null;

            var response = await _openStreetService.GetInfosByLatLon(lat, lon);

            return response;
        }
    }
}