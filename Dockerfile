FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["cineweb_user_api.csproj", "."]
RUN dotnet restore "./cineweb_user_api.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "cineweb_user_api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "cineweb_user_api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

RUN useradd -u 2737 well
USER well

CMD ASPNETCORE_URLS="http://*:$PORT" dotnet cineweb_user_api.dll