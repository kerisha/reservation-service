#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["data/nuget.config", "data/"]
COPY ["data/data.csproj", "data/"]
RUN dotnet restore "data/data.csproj"
COPY . .
WORKDIR "/src/data"
RUN dotnet build "data.csproj" -c Release -o /src/build
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"