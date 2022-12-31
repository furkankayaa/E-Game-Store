FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["./Services.API/Services.API.csproj", "Services.API/"]
RUN dotnet restore "./Services.API/Services.API.csproj"
COPY ./Services.API ./Services.API
COPY ["/App.Library/App.Library.csproj", "App.Library/"]
COPY ./App.Library/ ./App.Library/

WORKDIR "/src/Services.API"
RUN dotnet build "Services.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Services.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Services.API.dll"]

ENV DBHOST=host.docker.internal