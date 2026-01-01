FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY ["./Shared/", "Shared/"]
COPY ["./Services/Server/src", "./Services/Server/src/"]

RUN dotnet publish ./Services/Server/src/PlServer.Server.API/PlServer.Server.API.csproj -c Release -o /.out


FROM mcr.microsoft.com/dotnet/aspnet:10.0

WORKDIR /app

COPY --from=build /.out .

ENTRYPOINT ["dotnet", "PlServer.Server.API.dll"]