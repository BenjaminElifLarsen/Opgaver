#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["ChatApp.csproj", "./"]
RUN dotnet restore "ChatApp.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "ChatApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ChatApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ARG arg0="localHost,1435"
ARG arg1="http://localHost:8080/"
ENTRYPOINT ["dotnet", "ChatApp.dll"]
CMD [$arg0, $arg1]
EXPOSE 80/tcp