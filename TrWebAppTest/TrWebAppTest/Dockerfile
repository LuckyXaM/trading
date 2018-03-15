FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY *.sln ./
COPY TrWebAppTest/TrWebAppTest/TrWebAppTest.csproj TrWebAppTest/TrWebAppTest/
RUN dotnet restore
COPY . .
WORKDIR /src/TrWebAppTest/TrWebAppTest
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TrWebAppTest.dll"]
