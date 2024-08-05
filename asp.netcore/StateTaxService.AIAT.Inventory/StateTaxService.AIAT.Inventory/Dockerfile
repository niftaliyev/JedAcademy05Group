FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /app
COPY *.csproj ./
COPY nuget.config ./
RUN dotnet restore
COPY . .
WORKDIR /app
RUN dotnet publish -c Release -o out

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish -c Release -o out

FROM base AS final
WORKDIR /app
COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "StateTaxService.AIAT.Inventory.dll"]