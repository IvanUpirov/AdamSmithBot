Telegram: @adamsm_bot

### Open the home directory
```
cd .\src\
```
### Build the image (optional)
```
docker build -f "\AdamSmith.WebApi\Dockerfile" -t ivupi/adamsmithwebapi:dev "D:\Sources\Other\AdamSmithBot\src"
```
### Run the container
```
docker run -p 80:80 ivupi/adamsmithwebapi:dev -e "ASPNETCORE_ENVIRONMENT=Development" -e "ASPNETCORE_URLS=http://+:80"
```
### Make a GET request to http://127.0.0.1/rate/usd
