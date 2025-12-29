FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY ["./Shared", "./Shared"]
COPY ["./Services/Worker/src", "./Services/Worker/src/"]

RUN dotnet publish ./Services/Worker/src/PlServer.Worker.API/PlServer.Worker.API.csproj -c Release -o /.out

FROM mcr.microsoft.com/dotnet/aspnet:10.0

WORKDIR /app

COPY --from=build .out .

EXPOSE 10000
ENV ASPNETCORE_URLS="http://+:10000"

ENTRYPOINT [ "dotnet", "PlServer.Worker.API.dll"]