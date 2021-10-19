Telegram: @adamsm_bot

## Docker build locally
```
cd .\src\
```
```
docker build -f "D:\Sources\Other\AdamSmithBot\src\AdamSmith.WebApi\Dockerfile" -t ivupi/adamsmithwebapi:dev "D:\Sources\Other\AdamSmithBot\src"
```
```
docker run -p 80:80 adamsmithwebapi:dev -e "ASPNETCORE_ENVIRONMENT=Development" -e "ASPNETCORE_URLS=http://+:80"
```
http://127.0.0.1/rate/usd
