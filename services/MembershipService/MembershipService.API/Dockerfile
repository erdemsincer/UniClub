FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY MembershipService.sln .

COPY MembershipService.API/MembershipService.API.csproj MembershipService.API/
COPY MembershipService.Application/MembershipService.Application.csproj MembershipService.Application/
COPY MembershipService.Domain/MembershipService.Domain.csproj MembershipService.Domain/
COPY MembershipService.Infrastructure/MembershipService.Infrastructure.csproj MembershipService.Infrastructure/
COPY MembershipService.Persistence/MembershipService.Persistence.csproj MembershipService.Persistence/

RUN dotnet restore MembershipService.sln

COPY . .
WORKDIR /src/MembershipService.API
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "MembershipService.API.dll"]
