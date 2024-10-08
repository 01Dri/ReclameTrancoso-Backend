# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar todos os projetos
COPY ./src/ReclameTrancoso.API/*.csproj ./ReclameTrancoso.API/
COPY ./src/ReclameTrancoso.Application/*.csproj ./ReclameTrancoso.Application/
COPY ./src/ReclameTrancoso.Domain/*.csproj ./ReclameTrancoso.Domain/
COPY ./src/ReclameTrancoso.Exceptions/*.csproj ./ReclameTrancoso.Exceptions/
COPY ./src/ReclameTrancoso.Infrastructure/*.csproj ./ReclameTrancoso.Infrastructure/
COPY ./src/ReclameTrancoso.IoC/*.csproj ./ReclameTrancoso.IoC/

# Restore para todas as dependências
RUN dotnet restore ./ReclameTrancoso.API/ReclameTrancoso.API.csproj

# Copiar os arquivos da aplicação
COPY ./src/ReclameTrancoso.API/. ./ReclameTrancoso.API/
COPY ./src/ReclameTrancoso.Application/. ./ReclameTrancoso.Application/
COPY ./src/ReclameTrancoso.Domain/. ./ReclameTrancoso.Domain/
COPY ./src/ReclameTrancoso.Exceptions/. ./ReclameTrancoso.Exceptions/
COPY ./src/ReclameTrancoso.Infrastructure/. ./ReclameTrancoso.Infrastructure/
COPY ./src/ReclameTrancoso.IoC/. ./ReclameTrancoso.IoC/

# Publicar a aplicação
RUN dotnet publish ./ReclameTrancoso.API/ReclameTrancoso.API.csproj -c Release -o /app/out

# Manter a estrutura de build para debug
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS debug
WORKDIR /src
COPY --from=build /src ./

# Etapa de execução
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Comando de execução da aplicação
ENTRYPOINT ["dotnet", "ReclameTrancoso.API.dll"]
