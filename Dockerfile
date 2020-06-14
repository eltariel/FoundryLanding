FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY Foundry.Landing/*.csproj Foundry.Landing/
COPY Foundry.WorldReader/*.csproj Foundry.WorldReader/
COPY Foundry.WorldReader.Tester/*.csproj Foundry.WorldReader.Tester/
COPY *.sln ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /foundry/data
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Foundry.Landing.dll"]
