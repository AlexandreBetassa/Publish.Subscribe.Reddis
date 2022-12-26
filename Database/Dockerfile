FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY  *.sln ./
COPY DatabaseAPI.csproj ./
RUN dotnet restore DatabaseAPI.csproj
COPY . .
RUN dotnet publish DatabaseAPI.csproj -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:7.0 
WORKDIR /app
COPY --from=build /out .
ENTRYPOINT [ "dotnet", "DatabaseAPI.dll"]
EXPOSE 80
EXPOSE 443
