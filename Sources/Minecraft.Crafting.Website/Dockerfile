FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ENV ASPNETCORE_ENVIRONMENT="Production"
WORKDIR /src
COPY ["Sources/Minecraft.Crafting.Website/Minecraft.Crafting.Website.csproj", "Minecraft.Crafting.Website/"]
RUN dotnet restore "Minecraft.Crafting.Website/Minecraft.Crafting.Website.csproj"
COPY . .
WORKDIR "Sources/Minecraft.Crafting.Website"
RUN dotnet build "Minecraft.Crafting.Website.csproj" -c Release -o /app/build

FROM build AS publish
ENV ASPNETCORE_ENVIRONMENT="Production"
RUN dotnet publish "Minecraft.Crafting.Website.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
ENV ASPNETCORE_ENVIRONMENT="Production"
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Minecraft.Crafting.Website.dll"]