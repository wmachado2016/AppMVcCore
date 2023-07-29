#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WSM.ResApi/WSM.ResApi.csproj", "WSM.ResApi/"]

RUN dotnet restore "WSM.ResApi/WSM.ResApi.csproj"
COPY . .
WORKDIR "/src/WSM.ResApi"
RUN dotnet build "WSM.ResApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WSM.ResApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WSM.ResApi.dll"]