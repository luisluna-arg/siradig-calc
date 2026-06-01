FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY backend/Core/core.csproj ./backend/Core/
COPY backend/Application/application.csproj ./backend/Application/
COPY backend/ApiFramework/apiFramework.csproj ./backend/ApiFramework/
COPY backend/Infra/infra.csproj ./backend/Infra/
COPY backend/Api/api.csproj ./backend/Api/
RUN dotnet restore backend/Api/api.csproj

COPY backend/ ./backend/
RUN dotnet publish backend/Api/api.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "api.dll"]
