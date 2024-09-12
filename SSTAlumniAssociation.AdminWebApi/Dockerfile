FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SSTAlumniAssociation.AdminWebApi/SSTAlumniAssociation.AdminWebApi.csproj", "SSTAlumniAssociation.AdminWebApi/"]
RUN dotnet restore "SSTAlumniAssociation.AdminWebApi/SSTAlumniAssociation.AdminWebApi.csproj"
COPY . .
WORKDIR "/src/SSTAlumniAssociation.AdminWebApi"
RUN dotnet build "SSTAlumniAssociation.AdminWebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "SSTAlumniAssociation.AdminWebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SSTAlumniAssociation.AdminWebApi.dll"]
