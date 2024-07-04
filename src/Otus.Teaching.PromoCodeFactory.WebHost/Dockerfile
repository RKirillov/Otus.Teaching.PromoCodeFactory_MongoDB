FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Otus.Teaching.PromoCodeFactory.WebHost.csproj", "./"]
RUN dotnet restore "Otus.Teaching.PromoCodeFactory.WebHost.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "Otus.Teaching.PromoCodeFactory.WebHost.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Otus.Teaching.PromoCodeFactory.WebHost.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Otus.Teaching.PromoCodeFactory.WebHost.csproj.dll"]