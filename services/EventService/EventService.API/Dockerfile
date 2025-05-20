FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# SLN ve .csproj dosyaları (tam kökten erişim)
COPY services/EventService/EventService.sln services/EventService/
COPY services/EventService/EventService.API/EventService.API.csproj services/EventService/EventService.API/
COPY services/EventService/EventService.Application/EventService.Application.csproj services/EventService/EventService.Application/
COPY services/EventService/EventService.Domain/EventService.Domain.csproj services/EventService/EventService.Domain/
COPY services/EventService/EventService.Infrastructure/EventService.Infrastructure.csproj services/EventService/EventService.Infrastructure/
COPY services/EventService/EventService.Persistence/EventService.Persistence.csproj services/EventService/EventService.Persistence/

# 🔥 Shared projesi (NotificationService mantığına uygun şekilde köke kopyalanıyor)
COPY UniClub.Shared/ UniClub.Shared/

# Restore
WORKDIR /src/services/EventService
RUN dotnet restore EventService.sln

# Kaynak klasörleri tek tek kopyala (build cache daha verimli olur)
COPY services/EventService/EventService.API/ EventService.API/
COPY services/EventService/EventService.Application/ EventService.Application/
COPY services/EventService/EventService.Domain/ EventService.Domain/
COPY services/EventService/EventService.Infrastructure/ EventService.Infrastructure/
COPY services/EventService/EventService.Persistence/ EventService.Persistence/
COPY UniClub.Shared/ UniClub.Shared/

# Publish
WORKDIR /src/services/EventService/EventService.API
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "EventService.API.dll"]
