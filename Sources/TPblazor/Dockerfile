FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Sources/TPblazor/TPblazor.csproj", "TPblazor/"]
RUN dotnet restore "TPblazor/TPblazor.csproj"
COPY . .
WORKDIR "Sources/TPblazor"
RUN dotnet build "TPblazor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TPblazor.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TPblazor.dll"]