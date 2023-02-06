FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Tryitter/Tryitter.csproj", "Tryitter/"]
RUN dotnet restore "Tryitter/Tryitter.csproj"
COPY . .
WORKDIR "/src/Tryitter"
RUN dotnet build "Tryitter.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tryitter.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tryitter.dll"]
