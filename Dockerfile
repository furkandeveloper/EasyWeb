#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY common.props ./
COPY ["sample/EasyWeb.Demo/EasyWeb.Demo.csproj", "sample/EasyWeb.Demo/"]
COPY ["src/EasyWeb.AspNetCore.Swagger/EasyWeb.AspNetCore.Swagger.csproj", "src/EasyWeb.AspNetCore.Swagger/"]
COPY ["src/EasyWeb.AspNetCore.ApiStandarts/EasyWeb.AspNetCore.ApiStandarts.csproj", "src/EasyWeb.AspNetCore.ApiStandarts/"]
COPY ["src/EasyWeb.AspNetCore.Filters/EasyWeb.AspNetCore.Filters.csproj", "src/EasyWeb.AspNetCore.Filters/"]
COPY ["src/EasyWeb.AspNetCore/EasyWeb.AspNetCore.csproj", "src/EasyWeb.AspNetCore/"]
COPY ["src/EasyWeb.Core/EasyWeb.Core.csproj", "src/EasyWeb.Core/"]
RUN dotnet restore "sample/EasyWeb.Demo/EasyWeb.Demo.csproj"
COPY . .
WORKDIR "/src/sample/EasyWeb.Demo"
RUN dotnet build "EasyWeb.Demo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EasyWeb.Demo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EasyWeb.Demo.dll"]