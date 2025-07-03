# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src

# Copiar archivos de solución y proyectos
COPY ["ExtraHours.sln", "."]
COPY ["ExtraHours.Api/ExtraHours.Api.csproj", "ExtraHours.Api/"]
COPY ["ExtraHours.Core/ExtraHours.Core.csproj", "ExtraHours.Core/"]
COPY ["ExtraHours.Infrastructure/ExtraHours.Infrastructure.csproj", "ExtraHours.Infrastructure/"]

# Restaurar dependencias
RUN dotnet restore "ExtraHours.Api/ExtraHours.Api.csproj"

# Copiar el resto de los archivos
COPY . .

# Publicar la aplicación
WORKDIR "/src/ExtraHours.Api"
RUN dotnet publish -c Release -o /app/publish

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime

WORKDIR /app

# Exponer puerto 5011
EXPOSE 5011

# Establecer la URL de escucha dentro del contenedor
ENV ASPNETCORE_URLS=http://+:5011

# Copiar archivos publicados desde la etapa build
COPY --from=build /app/publish .

# Comando de inicio
ENTRYPOINT ["dotnet", "ExtraHours.Api.dll"]

