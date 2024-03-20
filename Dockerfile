FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["align/align.csproj", "align/"]
RUN dotnet restore "align/align.csproj"
COPY . .
WORKDIR "/src/align"
ENV ASPNETCORE_ENVIRONMENT=Production
RUN dotnet build "align.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "align.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "align.dll"]