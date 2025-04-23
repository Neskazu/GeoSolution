using Microsoft.AspNetCore.Mvc;
using GeoSolution.Models;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using GeoSolution.Services;
using Microsoft.AspNetCore.Components.RenderTree;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;

namespace GeoSolution.Controllers
{
    public class MapController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly ILogger<MapController> _logger;
        public MapController(IConfiguration configuration, IHttpClientFactory httpClientFactory, ILogger<MapController> logger)
        {
            _configuration = configuration;
            _httpClient = httpClientFactory.CreateClient();
            _logger = logger;
        }
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetFeatureInfo(
            double x, double y,
            double bboxMinX, double bboxMinY, double bboxMaxX, double bboxMaxY,
            int width, int height)
        {
            string geoServerUrl = _configuration["GeoServer:Url"];     
            string layerName = _configuration["GeoServer:BuildingsLayerName"];
            var url = FormattableString.Invariant($"{geoServerUrl}service=WMS&version=1.1.1&request=GetFeatureInfo&layers={layerName}&query_layers={layerName}&styles=&bbox={bboxMinX},{bboxMinY},{bboxMaxX},{bboxMaxY}&width={width}&height={height}&srs=EPSG:4326&x={x}&y={y}&info_format=application/json");
            
            // Отправка запроса к GeoServer
            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, "Ошибка при получении данных GetFeatureInfo");
            }
            var jsonContent = await response.Content.ReadAsStringAsync();
            //deserialize
            var jObject = JObject.Parse(jsonContent);
            var firstFeature = jObject["features"]?.FirstOrDefault();
            if (firstFeature == null)
            {
                return NotFound("not a object");
            }
            var properties = firstFeature["properties"];
            FeatureInfoBuildingModel featureInfo = new FeatureInfoBuildingModel
            {
                Name = properties["Name"]?.ToString(),
                BuildingType = properties["BuildingType"]?.ToString(),
                Description = properties["Description"]?.ToString(),
                CurrentStatus = properties["CurrentStatus"]?.ToString(),
                OpeningHours = properties["OpeningHours"]?.ToString()
            };
            return PartialView("_FeatureInfoPopup", featureInfo);

        }
    }
}