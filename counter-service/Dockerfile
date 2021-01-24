#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["counter-service.csproj", ""]
RUN dotnet restore "./counter-service.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "counter-service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "counter-service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "counter-service.dll"]