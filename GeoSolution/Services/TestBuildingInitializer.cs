using GeoSolution.Data;
using GeoSolution.Models;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace GeoSolution.Services
{
    public class TestBuildingInitializer
    {
        public static async Task InitializeAsync(ApplicationDbContext dbContext)
        {
            // Убедимся, что база данных существует
            await dbContext.Database.EnsureCreatedAsync();

            // Проверим, есть ли уже данные
            if (await dbContext.CustomBuildings.AnyAsync())
                return;

            // Пример геометрии — полигон (здание)
            var polygon = new Polygon(new LinearRing(new[]
            {
    new Coordinate(73.29681237360631, 55.0180168315672), // Долгота, Широта
    new Coordinate(73.29410394611033, 55.01670750959449), // Долгота, Широта
    new Coordinate(73.29809507302586, 55.015600270310046), // Долгота, Широта
    new Coordinate(73.29985460209615, 55.017224210796485), // Долгота, Широта
    new Coordinate(73.29681237360631, 55.0180168315672)  // Долгота, Широта
}));

            var multiPolygon = new MultiPolygon(new[] { polygon });
            // Создаем тестовые здания
            var buildings = new[]
            {
                new CustomBuildingModel
                {
                    Name = "Тестовое здание 1",
                    Description = "Описание здания 1",
                    Geometry = multiPolygon,
                    BuildingType = "Office",
                    CurrentStatus = "Active",
                    OpeningHours = "8:00-18:00",
                    Color = "#f54242"
                }
            };

            // Сохраняем данные в базу
            dbContext.CustomBuildings.AddRange(buildings);
            await dbContext.SaveChangesAsync();
        }
    }
}
