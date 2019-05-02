FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY WebApi/WebApi.csproj WebApi/
RUN dotnet restore WebApi/WebApi.csproj
COPY . .
WORKDIR /src/WebApi
RUN dotnet build WebApi.csproj --no-restore -c Release -o /app

FROM build as test
WORKDIR /src/WebApi.Tests


FROM build AS publish
RUN dotnet publish WebApi.csproj --no-restore -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WebApi.dll"]
