# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copier les fichiers projet
COPY ["TaskManagerAPI.csproj", "."]

# Restore dependencies
RUN dotnet restore "TaskManagerAPI.csproj"

# Copier le code source
COPY . .

# Build
RUN dotnet build "TaskManagerAPI.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "TaskManagerAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Health check endpoint
HEALTHCHECK --interval=30s --timeout=10s --start-period=60s --retries=3 \
    CMD dotnet /app/TaskManager.API.dll || exit 1

EXPOSE 5001
ENV ASPNETCORE_URLS=http://+:5001

ENTRYPOINT ["dotnet", "TaskManager.API.dll"]
