FROM mcr.microsoft.com/dotnet/sdk:latest AS build
WORKDIR /WebApp

# copy csproj and restore as distinct layers
COPY *.sln .
COPY *.props .

# copy ALL the projects
COPY App.BLL/*.csproj ./App.BLL/
COPY App.BLL.DTO/*.csproj ./App.BLL.DTO/
COPY App.Contracts/*.csproj ./App.Contracts/
COPY App.DAL.EF/*.csproj ./App.DAL.EF/
COPY App.DTO/*.csproj ./App.DTO/
COPY App.Public.DTO/*.csproj ./App.Public.DTO/
COPY App.Resources/*.csproj ./App.Resources/
COPY WebApp/*.csproj ./WebApp/
COPY App.Contracts.BLL/*.csproj ./App.Contracts.BLL/
COPY App.Domain/*.csproj ./App.Domain/

COPY Base.BLL/*.csproj ./Base.BLL/
COPY Base.DAL/*.csproj ./Base.DAL/
COPY Base.DAL.EF/*.csproj ./Base.DAL.EF/
COPY Base.Domain/*.csproj ./Base.Domain/
COPY Base.Extensions/*.csproj ./Base.Extensions/

COPY Base.Contracts.Base/*.csproj ./Base.Contracts.Base/
COPY Base.Contracts.DAL/*.csproj ./Base.Contracts.DAL/
COPY Base.Contracts.BLL/*.csproj ./Base.Contracts.BLL/
COPY Base.Contracts.Domain/*.csproj ./Base.Contracts.Domain/

RUN dotnet restore

COPY App.BLL/. ./App.BLL/
COPY App.BLL.DTO/. ./App.BLL.DTO/
COPY App.Contracts/. ./App.Contracts/
COPY App.DAL.EF/. ./App.DAL.EF/
COPY App.DTO/. ./App.DTO/
COPY App.Public.DTO/. ./App.Public.DTO/
COPY App.Resources/. ./App.Resources/
COPY WebApp/. ./WebApp/
COPY App.Contracts.BLL/. ./App.Contracts.BLL/
COPY App.Domain/. ./App.Domain/

COPY Base.BLL/. ./Base.BLL/
COPY Base.DAL/. ./Base.DAL/
COPY Base.DAL.EF/. ./Base.DAL.EF/
COPY Base.Domain/. ./Base.Domain/
COPY Base.Extensions/. ./Base.Extensions/

COPY Base.Contracts.Base/. ./Base.Contracts.Base/
COPY Base.Contracts.DAL/. ./Base.Contracts.DAL/
COPY Base.Contracts.BLL/. ./Base.Contracts.BLL/
COPY Base.Contracts.Domain/. ./Base.Contracts.Domain/

WORKDIR /WebApp/WebApp
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:latest AS runtime
WORKDIR /app
COPY --from=build /WebApp/WebApp/out ./
ENTRYPOINT ["dotnet", "WebApp.dll"]