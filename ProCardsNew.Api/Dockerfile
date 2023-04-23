FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ProCardsNew.Api/ProCardsNew.Api.csproj", "ProCardsNew.Api/"]
COPY ["ProCardsNew.Application/ProCardsNew.Application.csproj", "ProCardsNew.Application/"]
COPY ["ProCardsNew.Domain/ProCardsNew.Domain.csproj", "ProCardsNew.Domain/"]
COPY ["ProCardsNew.Contracts/ProCardsNew.Contracts.csproj", "ProCardsNew.Contracts/"]
COPY ["ProCardsNew.Infrastructure/ProCardsNew.Infrastructure.csproj", "ProCardsNew.Infrastructure/"]
RUN dotnet restore "ProCardsNew.Api/ProCardsNew.Api.csproj"
COPY . .
WORKDIR "/src/ProCardsNew.Api"
RUN dotnet build "ProCardsNew.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProCardsNew.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=build /src /src
ENTRYPOINT ["dotnet", "ProCardsNew.Api.dll"]
