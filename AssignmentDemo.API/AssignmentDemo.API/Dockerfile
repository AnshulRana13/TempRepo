#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

  
# Configure the run time environment
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

# Configure the build process
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["AssignmentDemo.API/AssignmentDemo.API/AssignmentDemo.API.csproj", "AssignmentDemo.API/AssignmentDemo.API/"]
COPY ["AssignmentDemo.API/AssignmentDemo.Entities.API/AssignmentDemo.Entities.API.csproj", "AssignmentDemo.API/AssignmentDemo.Entities.API/"]
COPY ["AssignmentDemo.API/AssignmentDemo.Provider/AssignmentDemo.Provider.csproj", "AssignmentDemo.API/AssignmentDemo.Provider/"]
RUN dotnet restore "AssignmentDemo.API/AssignmentDemo.API/AssignmentDemo.API.csproj"
COPY . .

# Trigger the restore/build of the project
WORKDIR "/src/AssignmentDemo.API"
RUN dotnet build "AssignmentDemo.API/AssignmentDemo.API.csproj" -c Release -o /app/build

# With the build completed, publish the application
FROM build AS publish
RUN dotnet publish "AssignmentDemo.API/AssignmentDemo.API.csproj" -c Release -o /app/publish

# Now execute the worker using dotnet runtimes
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Configure Kestral Envrionment
#ENV ASPNETCORE_URLS http://+:5000
#EXPOSE 5000

#Execute program
ENTRYPOINT ["dotnet", "AssignmentDemo.API.dll"]