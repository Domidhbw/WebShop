# Use the official ASP.NET Core runtime as a base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Use the SDK image to build the project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ShoppingCartDB.API/ShoppingCartDB.API.csproj ShoppingCartDB.API/
RUN dotnet restore "ShoppingCartDB.API/ShoppingCartDB.API.csproj"
COPY . .
WORKDIR "/src/ShoppingCartDB.API"
RUN dotnet build "ShoppingCartDB.API.csproj" -c Release -o /app/build

# Publish the project
FROM build AS publish
RUN dotnet publish "ShoppingCartDB.API.csproj" -c Release -o /app/publish

# Final stage: copy the build output to the runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShoppingCartDB.API.dll"]