#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

COPY . ./
RUN dotnet restore 

COPY . ./
RUN dotnet publish -c Release -o publish

FROM mcr.microsoft.com/dotnet/aspnet:3.1
WORKDIR /app
EXPOSE 5000
COPY --from=build /app/publish .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet CardScheme.Api.dll
#ENTRYPOINT ["dotnet", "CardScheme.Api.dll"]