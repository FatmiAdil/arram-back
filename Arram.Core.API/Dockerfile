#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Arram.Core.API/Arram.Core.API.csproj", "Arram.Core.API/"]
RUN dotnet restore "Arram.Core.API/Arram.Core.API.csproj"
COPY . .
WORKDIR "/src/Arram.Core.API"
RUN dotnet build "Arram.Core.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Arram.Core.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Arram.Core.API.dll"]