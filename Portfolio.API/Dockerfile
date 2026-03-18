# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copier les fichiers projet
COPY ["Portfolio.API/Portfolio.API.csproj", "Portfolio.API/"]
COPY ["Portfolio.Application/Portfolio.Application.csproj", "Portfolio.Application/"]
COPY ["Portfolio.Domain/Portfolio.Domain.csproj", "Portfolio.Domain/"]
COPY ["Portfolio.Infrastructure/Portfolio.Infrastructure.csproj", "Portfolio.Infrastructure/"]

# Restore dependencies
RUN dotnet restore "Portfolio.API/Portfolio.API.csproj"

# Copier tout le code source
COPY . .

# Build
WORKDIR "/src/Portfolio.API"
RUN dotnet build "Portfolio.API.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "Portfolio.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Health check endpoint
HEALTHCHECK --interval=30s --timeout=10s --start-period=60s --retries=3 \
    CMD dotnet /app/Portfolio.API.dll || exit 1

EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000

ENTRYPOINT ["dotnet", "Portfolio.API.dll"]
