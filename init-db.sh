#!/bin/bash
set -e

# Executar as migrations
dotnet ef database update

# Iniciar a aplicação
exec dotnet ReclameTrancoso.API.dll
