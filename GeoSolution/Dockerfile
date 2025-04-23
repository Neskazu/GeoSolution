# Этап для базового образа
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
# Если переменная APP_UID не установлена, либо задайте её, либо удалите эту строку
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Этап сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
# Копируем файл проекта, который находится в той же директории, что и Dockerfile
COPY ["GeoSolution.csproj", "./"]
RUN dotnet restore "./GeoSolution.csproj"
# Копируем все файлы проекта
COPY . .
# Если проект лежит прямо в /src, оставляем WORKDIR /src, иначе, если в поддиректории, переходим в неё:
# WORKDIR "/src/GeoSolution"
RUN dotnet build "./GeoSolution.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Этап публикации
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./GeoSolution.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Финальный этап
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GeoSolution.dll"]
