# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy project file and restore dependencies
COPY OrderApi.csproj .
RUN dotnet restore OrderApi.csproj

# Copy all files and build the app
COPY . .
RUN dotnet publish OrderApi.csproj -c Release -o /out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /out .

# Expose ports
EXPOSE 8080

# Start the application
ENTRYPOINT ["dotnet", "OrderApi.dll"]
