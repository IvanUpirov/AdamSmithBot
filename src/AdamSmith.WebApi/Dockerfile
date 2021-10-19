FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["AdamSmith.WebApi/AdamSmith.WebApi.csproj", "AdamSmith.WebApi/"]
RUN dotnet restore "AdamSmith.WebApi/AdamSmith.WebApi.csproj"
COPY . .
WORKDIR "/src/AdamSmith.WebApi"
RUN dotnet build "AdamSmith.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AdamSmith.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
ENV currency_url="https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?valcode={0}&date={1}&json"
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AdamSmith.WebApi.dll"]