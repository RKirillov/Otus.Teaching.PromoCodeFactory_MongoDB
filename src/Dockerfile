FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app 
EXPOSE 8080
#
# copy csproj and restore as distinct layers
COPY *.sln .
COPY Otus.Teaching.PromoCodeFactory.Core/*.csproj ./Otus.Teaching.PromoCodeFactory.Core/
COPY Otus.Teaching.PromoCodeFactory.DataAccess/*.csproj ./Otus.Teaching.PromoCodeFactory.DataAccess/
COPY Otus.Teaching.PromoCodeFactory.WebHost/*.csproj ./Otus.Teaching.PromoCodeFactory.WebHost/
#
RUN dotnet restore 
#
# copy everything else and build app
COPY Otus.Teaching.PromoCodeFactory.Core/. ./Otus.Teaching.PromoCodeFactory.Core/
COPY Otus.Teaching.PromoCodeFactory.DataAccess/. ./Otus.Teaching.PromoCodeFactory.DataAccess/
COPY Otus.Teaching.PromoCodeFactory.WebHost/. ./Otus.Teaching.PromoCodeFactory.WebHost/ 
#
WORKDIR /app/Otus.Teaching.PromoCodeFactory.WebHost
RUN dotnet publish -c Release -o out 
#
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app 
#

COPY --from=build /app/Otus.Teaching.PromoCodeFactory.WebHost/out ./
ENTRYPOINT ["dotnet", "Otus.Teaching.PromoCodeFactory.WebHost.dll"]