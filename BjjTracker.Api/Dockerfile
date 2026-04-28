FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["BjjTracker.Api/BjjTracker.Api.csproj", "BjjTracker.Api/"]
COPY ["BjjTracker.Application/BjjTracker.Application.csproj", "BjjTracker.Application/"]
COPY ["BjjTracker.Domain/BjjTracker.Domain.csproj", "BjjTracker.Domain/"]
COPY ["BjjTracker.Infrastructure/BjjTracker.Infrastructure.csproj", "BjjTracker.Infrastructure/"]
RUN dotnet restore "BjjTracker.Api/BjjTracker.Api.csproj"
COPY . .
WORKDIR "/src/BjjTracker.Api"
RUN dotnet build "BjjTracker.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BjjTracker.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BjjTracker.Api.dll"]