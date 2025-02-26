# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
EXPOSE 5179

# Copiar os arquivos .csproj de cada projeto
COPY Api.Application/Api.Application.csproj Api.Application/
COPY Api.CrossCutting/Api.CrossCutting.csproj Api.CrossCutting/
COPY Api.Data/Api.Data.csproj Api.Data/
COPY Api.Domain/Api.Domain.csproj Api.Domain/
COPY Api.Service/Api.Service.csproj Api.Service/
COPY Api.Service.Tests/Api.Service.Tests.csproj Api.Service.Tests/

# Restaurar dependências
RUN dotnet restore "Api.Application/Api.Application.csproj"

# Copiar o restante do código-fonte
COPY . .

# Compilar e publicar a aplicação
RUN dotnet publish "Api.Application/Api.Application.csproj" -c Release -o out

# Etapa final (runtime)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copiar os arquivos publicados da etapa de build
COPY --from=build /app/out .

# Definir o ponto de entrada da aplicação
ENTRYPOINT ["dotnet", "Api.Application.dll"]