#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DotNet.GitHubAction/DotNet.GitHubAction.csproj", "DotNet.GitHubAction/"]
RUN dotnet restore "DotNet.GitHubAction/DotNet.GitHubAction.csproj"
COPY . .
WORKDIR "/src/DotNet.GitHubAction"
RUN dotnet build "DotNet.GitHubAction.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DotNet.GitHubAction.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DotNet.GitHubAction.dll"]