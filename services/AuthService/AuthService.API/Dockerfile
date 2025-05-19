FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# .sln ve projeleri kopyala
COPY AuthService.sln .

COPY AuthService.API/AuthService.API.csproj AuthService.API/
COPY AuthService.Application/AuthService.Application.csproj AuthService.Application/
COPY AuthService.Domain/AuthService.Domain.csproj AuthService.Domain/
COPY AuthService.Infrastructure/AuthService.Infrastructure.csproj AuthService.Infrastructure/
COPY AuthService.Persistence/AuthService.Persistence.csproj AuthService.Persistence/

# restore yap
RUN dotnet restore AuthService.sln

# tüm dosyalarý kopyala
COPY . .

# build
WORKDIR /src/AuthService.API
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "AuthService.API.dll"]
