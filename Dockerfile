FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80/tcp

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

WORKDIR /src
COPY ["src/ChatRoomServer.WebApi/ChatRoomServer.WebApi.csproj", "ChatRoomServer.WebApi/"]
RUN dotnet restore "ChatRoomServer.WebApi/ChatRoomServer.WebApi.csproj"
COPY src .
WORKDIR "/src/ChatRoomServer.WebApi"
RUN dotnet build "ChatRoomServer.WebApi.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "ChatRoomServer.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChatRoomServer.WebApi.dll"]