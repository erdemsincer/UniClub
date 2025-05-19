FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ClubService.sln .

COPY ClubService.API/ClubService.API.csproj ClubService.API/
COPY ClubService.Application/ClubService.Application.csproj ClubService.Application/
COPY ClubService.Domain/ClubService.Domain.csproj ClubService.Domain/
COPY ClubService.Infrastructure/ClubService.Infrastructure.csproj ClubService.Infrastructure/
COPY ClubService.Persistence/ClubService.Persistence.csproj ClubService.Persistence/

RUN dotnet restore ClubService.sln

COPY . .
WORKDIR /src/ClubService.API
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ClubService.API.dll"]
