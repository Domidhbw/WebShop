# Use the official ASP.NET Core runtime as a base image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

# Use the SDK image to build the project
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ShoppingCart.API/ShoppingCart.API.csproj ShoppingCart.API/
RUN dotnet restore "ShoppingCart.API/ShoppingCart.API.csproj"
COPY . .
WORKDIR "/src/ShoppingCart.API"
RUN dotnet build "ShoppingCart.API.csproj" -c Release -o /app/build

# Publish the project
FROM build AS publish
RUN dotnet publish "ShoppingCart.API.csproj" -c Release -o /app/publish

# Final stage: copy the build output to the runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShoppingCart.API.dll"]