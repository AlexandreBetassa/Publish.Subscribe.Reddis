FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY  *.sln ./
COPY PubSubApi.csproj ./
RUN dotnet restore PubSubApi.csproj
COPY . .
RUN dotnet publish PubSubApi.csproj -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:7.0 
WORKDIR /app
COPY --from=build /out .
ENTRYPOINT [ "dotnet", "PubSubApi.dll"]
EXPOSE 80
EXPOSE 443
