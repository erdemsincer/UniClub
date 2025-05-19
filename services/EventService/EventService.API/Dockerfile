FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY EventService.sln .

COPY EventService.API/EventService.API.csproj EventService.API/
COPY EventService.Application/EventService.Application.csproj EventService.Application/
COPY EventService.Domain/EventService.Domain.csproj EventService.Domain/
COPY EventService.Infrastructure/EventService.Infrastructure.csproj EventService.Infrastructure/
COPY EventService.Persistence/EventService.Persistence.csproj EventService.Persistence/

RUN dotnet restore EventService.sln

COPY . .
WORKDIR /src/EventService.API
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "EventService.API.dll"]
