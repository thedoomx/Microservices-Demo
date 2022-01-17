FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Oxygen.Watchdog/Oxygen.Watchdog.csproj", "Oxygen.Watchdog/"]
COPY ["Oxygen.Startup.Common/Oxygen.Startup.Common.csproj", "Oxygen.Startup.Common/"]
COPY ["Oxygen.Common/Oxygen.Common.csproj", "Oxygen.Common/"]
COPY ["Oxygen.Infrastructure.Common/Oxygen.Infrastructure.Common.csproj", "Oxygen.Infrastructure.Common/"]
COPY ["Oxygen.Domain.Common/Oxygen.Domain.Common.csproj", "Oxygen.Domain.Common/"]
COPY ["Oxygen.Application.Common/Oxygen.Application.Common.csproj", "Oxygen.Application.Common/"]
COPY ["Oxygen.Web.Common/Oxygen.Web.Common.csproj", "Oxygen.Web.Common/"]
RUN dotnet restore "Oxygen.Watchdog/Oxygen.Watchdog.csproj"
COPY . .
WORKDIR "/src/Oxygen.Watchdog"
RUN dotnet build "Oxygen.Watchdog.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Oxygen.Watchdog.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Oxygen.Watchdog.dll"]