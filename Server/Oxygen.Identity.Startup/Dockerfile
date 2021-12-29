FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Oxygen.Identity.Startup/Oxygen.Identity.Startup.csproj", "Oxygen.Identity.Startup/"]
COPY ["Oxygen.Identity.Web/Oxygen.Identity.Web.csproj", "Oxygen.Identity.Web/"]
COPY ["Oxygen.Identity.Application/Oxygen.Identity.Application.csproj", "Oxygen.Identity.Application/"]
COPY ["Oxygen.Application.Common/Oxygen.Application.Common.csproj", "Oxygen.Application.Common/"]
COPY ["Ogyxen.Common/Ogyxen.Common.csproj", "Ogyxen.Common/"]
COPY ["Oxygen.Domain.Common/Oxygen.Domain.Common.csproj", "Oxygen.Domain.Common/"]
COPY ["Oxygen.Identity.Domain/Oxygen.Identity.Domain.csproj", "Oxygen.Identity.Domain/"]
COPY ["Oxygen.Web.Common/Oxygen.Web.Common.csproj", "Oxygen.Web.Common/"]
COPY ["Oxygen.Identity.Infrastructure/Oxygen.Identity.Infrastructure.csproj", "Oxygen.Identity.Infrastructure/"]
COPY ["Oxygen.Infrastructure.Common/Oxygen.Infrastructure.Common.csproj", "Oxygen.Infrastructure.Common/"]
COPY ["Oxygen.Startup.Common/Oxygen.Startup.Common.csproj", "Oxygen.Startup.Common/"]
RUN dotnet restore "Oxygen.Identity.Startup/Oxygen.Identity.Startup.csproj"
COPY . .
WORKDIR "/src/Oxygen.Identity.Startup"
RUN dotnet build "Oxygen.Identity.Startup.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Oxygen.Identity.Startup.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Oxygen.Identity.Startup.dll"]