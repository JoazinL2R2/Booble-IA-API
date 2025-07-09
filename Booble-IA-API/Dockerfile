# Use the official .NET 8 SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy csproj and restore dependencies
COPY ["Booble-IA-API/Booble-IA-API.csproj", "Booble-IA-API/"]
RUN dotnet restore "Booble-IA-API/Booble-IA-API.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/app/Booble-IA-API"
RUN dotnet build "Booble-IA-API.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "Booble-IA-API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Use the official .NET 8 runtime image for running
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Install curl for health checks (optional)
RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*

# Copy the published application
COPY --from=publish /app/publish .

# Create a non-root user for security
RUN adduser --disabled-password --gecos '' appuser && chown -R appuser /app
USER appuser

# Expose the port your app runs on
EXPOSE 8080

# Set environment variables
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

# Health check - ajustado para usar o endpoint correto
HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 \
    CMD curl -f http://localhost:8080/health || exit 1

# Set the entry point
ENTRYPOINT ["dotnet", "Booble-IA-API.dll"]