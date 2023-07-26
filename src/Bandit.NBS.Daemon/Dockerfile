#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Bandit.NBS.Daemon/Bandit.NBS.Daemon.csproj", "src/Bandit.NBS.Daemon/"]
COPY ["src/Bandit.NBS.MgdbRepository/Bandit.NBS.MgdbRepository.csproj", "src/Bandit.NBS.MgdbRepository/"]
COPY ["src/Bandit.NBS.NpgsqlRepository/Bandit.NBS.NpgsqlRepository.csproj", "src/Bandit.NBS.NpgsqlRepository/"]
RUN dotnet restore "src/Bandit.NBS.Daemon/Bandit.NBS.Daemon.csproj"
COPY . .
WORKDIR "/src/src/Bandit.NBS.Daemon"
RUN dotnet build "Bandit.NBS.Daemon.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Bandit.NBS.Daemon.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bandit.NBS.Daemon.dll"]