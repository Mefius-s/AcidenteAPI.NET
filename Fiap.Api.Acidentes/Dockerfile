# Use the .NET 8.0 runtime as the base for the final image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build the app in the SDK container
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy the project file and restore dependencies
COPY Fiap.Api.Acidentes.csproj ./
RUN dotnet restore "Fiap.Api.Acidentes.csproj"

COPY . .
WORKDIR "/src"
RUN dotnet build "Fiap.Api.Acidentes.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the application
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Fiap.Api.Acidentes.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Use the final runtime image
FROM base AS final
WORKDIR /app

# Copy the published app from the build container to the runtime container
COPY --from=publish /app/publish .

# Define the entry point for the container, using the PROFILE environment variable for ASPNETCORE_ENVIRONMENT
ENTRYPOINT ["dotnet", "Fiap.Api.Acidentes.dll"]
