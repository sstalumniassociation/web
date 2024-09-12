FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SSTAlumniAssociation.WebApi/SSTAlumniAssociation.WebApi.csproj", "SSTAlumniAssociation.WebApi/"]
RUN dotnet restore "SSTAlumniAssociation.WebApi/SSTAlumniAssociation.WebApi.csproj"
COPY . .
WORKDIR "/src/SSTAlumniAssociation.WebApi"
RUN dotnet build "SSTAlumniAssociation.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "SSTAlumniAssociation.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SSTAlumniAssociation.WebApi.dll"]
