#!/bin/bash

# Se a pasta ef não existir, baixa e extrai automaticamente
if [ ! -d "ef" ]; then
    echo "Baixando dotnet-ef (versão 8.0.6)..."
    wget -q https://www.nuget.org/api/v2/package/dotnet-ef/8.0.6 -O dotnet-ef.zip
    unzip -q dotnet-ef.zip -d ef
    rm dotnet-ef.zip
    echo "dotnet-ef pronto!"
fi

# Executa o comando dotnet-ef manualmente
dotnet ef/tools/net8.0/any/dotnet-ef.dll "$@"
