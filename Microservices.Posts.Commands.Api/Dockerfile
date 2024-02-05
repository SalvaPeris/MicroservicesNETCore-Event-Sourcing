#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Microservices.Posts.Commands.Api/Microservices.Posts.Commands.Api.csproj", "Microservices.Posts.Commands.Api/"]
COPY ["Microservices.Posts.Commands.Infrastructure/Microservices.Posts.Commands.Infrastructure.csproj", "Microservices.Posts.Commands.Infrastructure/"]
COPY ["Microservices.Posts.Commands.Domain/Microservices.Posts.Commands.Domain.csproj", "Microservices.Posts.Commands.Domain/"]
COPY ["Microservices.Posts.Common/Microservices.Posts.Common.csproj", "Microservices.Posts.Common/"]
COPY ["Microservices.Posts.CQRS/Microservices.Posts.CQRS.csproj", "Microservices.Posts.CQRS/"]
COPY ["Microservices.Posts.Queries.Domain/Microservices.Posts.Queries.Domain.csproj", "Microservices.Posts.Queries.Domain/"]
RUN dotnet restore "./Microservices.Posts.Commands.Api/./Microservices.Posts.Commands.Api.csproj"
COPY . .
WORKDIR "/src/Microservices.Posts.Commands.Api"
RUN dotnet build "./Microservices.Posts.Commands.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Microservices.Posts.Commands.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservices.Posts.Commands.Api.dll"]