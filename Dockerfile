FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build 
WORKDIR /App
COPY api/ .
RUN dotnet publish -o out -r linux-x64 -c Release

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runner
WORKDIR /App
COPY --from=build /App/out .
ENV DOTNET_EnableDiagnostics=0
ENV ASPNETCORE_URLS=http://+:80 
ENV ASPNETCORE_ENVIRONMENT=Development
ENTRYPOINT ["dotnet", "api.dll"]
