FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Alura.Challenge.BackEnd.Api/Alura.Challenge.BackEnd.Api.csproj", "Alura.Challenge.BackEnd.Api/"]
RUN dotnet restore "Alura.Challenge.BackEnd.Api/Alura.Challenge.BackEnd.Api.csproj"
COPY . .
WORKDIR "/src/Alura.Challenge.BackEnd.Api"
RUN dotnet build "Alura.Challenge.BackEnd.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet dev-certs https -ep %APPDATA%\ASP.NET\Https\Alura.Challenge.BackEnd.Api.pfx -p dK3zruo?.ZQ
RUN dotnet dev-certs https --trust
RUN dotnet publish "Alura.Challenge.BackEnd.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Alura.Challenge.BackEnd.Api.dll"]