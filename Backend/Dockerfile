FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /WebApp


# copy ALL the projects
COPY . .

RUN dotnet publish WebApp/WebApp.csproj -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /WebApp/out ./
ENTRYPOINT ["dotnet", "WebApp.dll"]