FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Signal.Server/Signal.Server.csproj", "Signal.Server/"]
RUN dotnet restore "Signal.Server/Signal.Server.csproj"
COPY . .
WORKDIR "/src/Signal.Server"
RUN dotnet build "Signal.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Signal.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Signal.Server.dll"]
