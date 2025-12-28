FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY ["./Shared", "./Shared"]
COPY ["./Services/Coordinator/src", "./Services/Coordinator/src/"]

RUN dotnet publish ./Services/Coordinator/src/PlServer.Coordinator.API/PlServer.Coordinator.API.csproj -c Release -o /.out

FROM mcr.microsoft.com/dotnet/aspnet:10.0

WORKDIR /app

COPY --from=build .out .

EXPOSE 50051
ENV ASPNETCORE_URLS="http://+:50051"

ENTRYPOINT [ "dotnet", "PlServer.Coordinator.API.dll"]