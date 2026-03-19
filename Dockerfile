FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copy the entire solution (all projects)
COPY . .

# Restore and publish the UI project
RUN dotnet restore "TaxpayerChecker/TaxpayerChecker.csproj"
RUN dotnet publish "TaxpayerChecker/TaxpayerChecker.csproj" -c Release -o /out

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /out .

EXPOSE 10000
ENV ASPNETCORE_URLS=http://+:10000

ENTRYPOINT ["dotnet", "TaxpayerChecker.dll"]
