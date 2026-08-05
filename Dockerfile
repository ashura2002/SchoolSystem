# stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:10.0-preview AS build

WORKDIR /src

# Copy only project files
COPY ["Application/Application.csproj", "Application/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["WebAPI/WebAPI.csproj", "WebAPI/"]

# Restore dependencies (cached unless .csproj changes)
RUN dotnet restore WebAPI/WebAPI.csproj

# Copy the remaining source code
COPY . .

# Publish the application
RUN dotnet publish WebAPI/WebAPI.csproj \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false

# stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0-preview AS runtime

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "WebAPI.dll"]