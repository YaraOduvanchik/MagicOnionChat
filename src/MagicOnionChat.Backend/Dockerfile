FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8081 8082

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY MagicOnionChat.Backend.csproj .
RUN dotnet restore MagicOnionChat.Backend.csproj
COPY . .
RUN dotnet publish MagicOnionChat.Backend.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "MagicOnionChat.Backend.dll"]
