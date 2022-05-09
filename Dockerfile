FROM mcr.microsoft.com/dotnet/sdk:6.0 as builder
WORKDIR /app/

COPY ./Signal.Server/Signal.Server.csproj /app/

RUN dotnet restore

COPY ./Signal.Server /app

RUN dotnet publish --output "/app/bin" -c release 

FROM mcr.microsoft.com/dotnet/aspnet:6.0

WORKDIR /app/bin

COPY --from=builder /app/bin /app/bin

CMD ["dotnet", "Signal.Server.dll"]
