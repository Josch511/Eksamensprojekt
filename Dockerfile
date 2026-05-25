# Use the official .NET build image for building (replace with the proper SDK version if needed)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

# Copy solution and restore dependencies
COPY Eksamensprojekt.slnx ./
COPY Core/Core.csproj Core/
COPY ServerAPI/ServerAPI.csproj ServerAPI/
COPY WebApp/WebApp.csproj WebApp/
RUN dotnet restore

# Copy all source code and build
COPY . ./
WORKDIR /app/ServerAPI
RUN dotnet publish -c Release -o /app/out

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/out ./

# If your API runs on a different port than 8080, set EXPOSE accordingly or add RENDER_EXTERNAL_PORT
ENV ASPNETCORE_URLS=http://+:8080

EXPOSE 8080

ENTRYPOINT ["dotnet", "ServerAPI.dll"]
