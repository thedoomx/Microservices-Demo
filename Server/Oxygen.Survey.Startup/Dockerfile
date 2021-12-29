FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Oxygen.Survey.Startup/Oxygen.Survey.Startup.csproj", "Oxygen.Survey.Startup/"]
COPY ["Oxygen.Survey.Application/Oxygen.Survey.Application.csproj", "Oxygen.Survey.Application/"]
COPY ["Oxygen.Survey.Domain/Oxygen.Survey.Domain.csproj", "Oxygen.Survey.Domain/"]
COPY ["Oxygen.Domain.Common/Oxygen.Domain.Common.csproj", "Oxygen.Domain.Common/"]
COPY ["Ogyxen.Common/Ogyxen.Common.csproj", "Ogyxen.Common/"]
COPY ["Oxygen.Infrastructure.Common/Oxygen.Infrastructure.Common.csproj", "Oxygen.Infrastructure.Common/"]
COPY ["Oxygen.Application.Common/Oxygen.Application.Common.csproj", "Oxygen.Application.Common/"]
COPY ["Oxygen.Survey.Infrastructure/Oxygen.Survey.Infrastructure.csproj", "Oxygen.Survey.Infrastructure/"]
COPY ["Oxygen.Survey.Web/Oxygen.Survey.Web.csproj", "Oxygen.Survey.Web/"]
COPY ["Oxygen.Web.Common/Oxygen.Web.Common.csproj", "Oxygen.Web.Common/"]
COPY ["Oxygen.Startup.Common/Oxygen.Startup.Common.csproj", "Oxygen.Startup.Common/"]
RUN dotnet restore "Oxygen.Survey.Startup/Oxygen.Survey.Startup.csproj"
COPY . .
WORKDIR "/src/Oxygen.Survey.Startup"
RUN dotnet build "Oxygen.Survey.Startup.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Oxygen.Survey.Startup.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Oxygen.Survey.Startup.dll"]