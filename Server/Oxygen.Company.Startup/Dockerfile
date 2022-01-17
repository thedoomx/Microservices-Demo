FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Oxygen.Company.Startup/Oxygen.Company.Startup.csproj", "Oxygen.Company.Startup/"]
COPY ["Oxygen.Company.Domain/Oxygen.Company.Domain.csproj", "Oxygen.Company.Domain/"]
COPY ["Oxygen.Domain.Common/Oxygen.Domain.Common.csproj", "Oxygen.Domain.Common/"]
COPY ["Oxygen.Common/Oxygen.Common.csproj", "Oxygen.Common/"]
COPY ["Oxygen.Company.Web/Oxygen.Company.Web.csproj", "Oxygen.Company.Web/"]
COPY ["Oxygen.Company.Application/Oxygen.Company.Application.csproj", "Oxygen.Company.Application/"]
COPY ["Oxygen.Application.Common/Oxygen.Application.Common.csproj", "Oxygen.Application.Common/"]
COPY ["Oxygen.Web.Common/Oxygen.Web.Common.csproj", "Oxygen.Web.Common/"]
COPY ["Oxygen.Company.Infrastructure/Oxygen.Company.Infrastructure.csproj", "Oxygen.Company.Infrastructure/"]
COPY ["Oxygen.Infrastructure.Common/Oxygen.Infrastructure.Common.csproj", "Oxygen.Infrastructure.Common/"]
COPY ["Oxygen.Startup.Common/Oxygen.Startup.Common.csproj", "Oxygen.Startup.Common/"]
RUN dotnet restore "Oxygen.Company.Startup/Oxygen.Company.Startup.csproj"
COPY . .
WORKDIR "/src/Oxygen.Company.Startup"
RUN dotnet build "Oxygen.Company.Startup.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Oxygen.Company.Startup.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Oxygen.Company.Startup.dll"]