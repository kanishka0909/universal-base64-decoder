# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy everything
COPY . ./

# Restore + publish
RUN dotnet publish src/UniversalBase64Decoder.Web/UniversalBase64Decoder.Web.csproj -c Release -o out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/out ./

EXPOSE 8080
ENTRYPOINT ["dotnet", "UniversalBase64Decoder.Web.dll"]
