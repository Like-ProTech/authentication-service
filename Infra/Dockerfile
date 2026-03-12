FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app
COPY ["Authentication-Service.csproj", "./"]
RUN dotnet restore "Authentication-Service.csproj"
COPY . .
RUN dotnet publish "Authentication-Service.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Authentication-Service.dll"]