#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WSM.Testes/WSM.Testes.csproj", "WSM.Testes/"]
COPY ["WSM.ResApi/WSM.ResApi.csproj", "WSM.ResApi/"]
RUN dotnet restore "WSM.Testes/WSM.Testes.csproj"
COPY . .
WORKDIR "/src/WSM.Testes"
RUN dotnet build "WSM.Testes.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WSM.Testes.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WSM.Testes.dll"]