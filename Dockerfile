# Build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy solution and projects for restore
COPY Eksamensprojekt.slnx ./
COPY Core/Core.csproj Core/
COPY ServerAPI/ServerAPI.csproj ServerAPI/
COPY WebApp/WebApp.csproj WebApp/

RUN dotnet restore Eksamensprojekt.slnx

# Copy everything else and build
COPY . ./
WORKDIR /app/ServerAPI
RUN dotnet publish -c Release -o /app/out

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/out ./
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "ServerAPI.dll"]
