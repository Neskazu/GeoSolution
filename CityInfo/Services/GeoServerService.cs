
using CityInfo.Models;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Drawing;
using System;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace CityInfo.Services
{
    public class GeoServerService
    {
        private readonly HttpClient httpClient;
        private readonly string geoServerUrl;
        private readonly string workSpace;
        private readonly string layerName;

        public GeoServerService(string geoServerUrl, string workSpace, string layerName)
        {
            httpClient = new HttpClient();
            this.geoServerUrl = geoServerUrl;
            this.workSpace = workSpace;
            this.layerName = layerName;
        }
        public async Task<int> AddGisObjectAsync(CustomBuildingModel customBuildingModel)
        {
            var featureXml = $@"
   <wfs:Transaction service=""WFS"" version=""2.0.0""
    xmlns:wfs=""http://www.opengis.net/wfs/2.0""
    xmlns:gml=""http://www.opengis.net/gml/3.2""
    xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
    xmlns:{workSpace}=""{workSpace}""
    xsi:schemaLocation=""http://www.opengis.net/wfs/2.0 http://schemas.opengis.net/wfs/2.0/wfs.xsd"">
    
    <wfs:Insert>
        <{workSpace}:{layerName}>
            <{workSpace}:geom>
                <gml:MultiPolygon srsName=""EPSG:4326"">
                    <gml:polygonMember>
                        <gml:Polygon>
                            <gml:exterior>
                                <gml:LinearRing>
                                    <gml:posList>
                                        {customBuildingModel.Geometry}
                                    </gml:posList>
                                </gml:LinearRing>
                            </gml:exterior>
                        </gml:Polygon>
                    </gml:polygonMember>
                </gml:MultiPolygon>
            </{workSpace}:geom>
            <{workSpace}:name>{customBuildingModel.Name}</{workSpace}:name>
            <{workSpace}:type>{customBuildingModel.BuildingType}</{workSpace}:type>
        </{workSpace}:gis_osm_buildings_a_free_1>
    </wfs:Insert>
</wfs:Transaction>";
            var content = new StringContent(featureXml, Encoding.UTF8, "application/xml");

            var requestUri = $"{geoServerUrl}/geoserver/wfs";
            var response = await httpClient.PostAsync(requestUri, content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Geoserver Error: {responseContent}");
            }
            Console.WriteLine(responseContent);
            var gid = ParseGidFromResponse(responseContent);
            return gid;
        }
        private int ParseGidFromResponse(string responseContent)
        {
            try
            {
                // Загружаем XML-ответ с помощью XDocument
                XDocument xmlDoc = XDocument.Parse(responseContent);

                // Определяем пространство имен fes
                XNamespace fes = "http://www.opengis.net/fes/2.0";

                // Ищем атрибут rid в элементе ResourceId
                var ridAttribute = xmlDoc.Descendants(fes + "ResourceId")
                                         .Attributes("rid")
                                         .FirstOrDefault();

                if (ridAttribute != null)
                {
                    string rid = ridAttribute.Value;
                    // Извлекаем числовую часть после точки, если присутствует
                    if (rid.Contains("."))
                    {
                        return int.Parse(rid.Split('.')[1]);
                    }
                }

                throw new Exception("GID не найден в ответе GeoServer.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при парсинге ответа от GeoServer: {ex.Message}");
            }
        }
    }
}
