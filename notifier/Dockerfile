#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["notifier/nuget.config", "notifier/"]
COPY ["notifier/notifier.csproj", "notifier/"]
RUN dotnet restore "notifier/notifier.csproj"
COPY . .
WORKDIR "/src/notifier"
RUN dotnet build "notifier.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "notifier.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
RUN apt-get update && apt-get install -y wget
RUN wget -O /wait-for-it.sh https://raw.githubusercontent.com/vishnubob/wait-for-it/master/wait-for-it.sh
RUN chmod +x /wait-for-it.sh 
COPY --from=publish /app/publish .