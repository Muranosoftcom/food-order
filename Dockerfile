FROM microsoft/dotnet:2.2-sdk AS build-env

RUN apt-get update -y --no-install-recommends \
        && apt-get install curl -yq \
        && curl -sL https://deb.nodesource.com/setup_10.x | bash \
        && apt-get install nodejs -yq

RUN npm install yarn -g

WORKDIR /app
COPY . .

RUN dotnet publish ./WebUI/WebUI.csproj -c Release -o out 

# Build runtime image
FROM microsoft/dotnet:2.2-aspnetcore-runtime AS runtime
WORKDIR /app
COPY --from=build-env /app/WebUI/out .
ENTRYPOINT ["dotnet", "WebUI.dll"]
