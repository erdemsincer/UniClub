FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# SLN ve projeleri kopyala
COPY services/NotificationService/NotificationService.sln services/NotificationService/
COPY services/NotificationService/NotificationService.API/NotificationService.API.csproj services/NotificationService/NotificationService.API/
COPY services/NotificationService/NotificationService.Application/NotificationService.Application.csproj services/NotificationService/NotificationService.Application/
COPY services/NotificationService/NotificationService.Domain/NotificationService.Domain.csproj services/NotificationService/NotificationService.Domain/
COPY services/NotificationService/NotificationService.Infrastructure/NotificationService.Infrastructure.csproj services/NotificationService/NotificationService.Infrastructure/

# 🔥 Shared klasörünü tam köke kopyala (/src/UniClub.Shared/)
COPY UniClub.Shared/ UniClub.Shared/

# Restore
WORKDIR /src/services/NotificationService
RUN dotnet restore NotificationService.sln

# Kaynakları kopyala
COPY services/NotificationService/NotificationService.API/ NotificationService.API/
COPY services/NotificationService/NotificationService.Application/ NotificationService.Application/
COPY services/NotificationService/NotificationService.Domain/ NotificationService.Domain/
COPY services/NotificationService/NotificationService.Infrastructure/ NotificationService.Infrastructure/

# Publish
WORKDIR /src/services/NotificationService/NotificationService.API
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "NotificationService.API.dll"]
