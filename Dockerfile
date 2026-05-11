FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["LibraryAPI.WebAPI/LibraryAPI.WebAPI.csproj", "LibraryAPI.WebAPI/"]
RUN dotnet restore "LibraryAPI.WebAPI/LibraryAPI.WebAPI.csproj"
COPY . .
WORKDIR "/src/LibraryAPI.WebAPI"
RUN dotnet build "./LibraryAPI.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./LibraryAPI.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LibraryAPI.WebAPI.dll"]
