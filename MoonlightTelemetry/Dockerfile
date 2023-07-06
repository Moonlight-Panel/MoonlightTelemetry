FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MoonlightTelemetry/MoonlightTelemetry.csproj", "MoonlightTelemetry/"]
RUN dotnet restore "MoonlightTelemetry/MoonlightTelemetry.csproj"
COPY . .
WORKDIR "/src/MoonlightTelemetry"
RUN dotnet build "MoonlightTelemetry.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MoonlightTelemetry.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MoonlightTelemetry.dll"]
