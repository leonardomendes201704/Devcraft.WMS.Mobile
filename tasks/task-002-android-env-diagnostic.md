# Tarefa 002: Diagnosticar configuração Android (.NET MAUI)

## Controle de tempo
- Início: 2026-02-05 11:45
- Fim: 2026-02-05 12:09
- Duração: 00:24


## Objetivo
Executar diagnósticos no ambiente para identificar por que o build Android foi cancelado.

## Contexto
- Usuário autorizou seguir com diagnóstico.
- Erro anterior: "O ambiente Android precisa ser configurado".

## Escopo
- Verificar workloads do .NET
- Verificar variáveis de ambiente Android/JDK
- Verificar caminhos padrão do Android SDK
- Tentar build Android e capturar log

## Fora do escopo
- Instalação de componentes sem confirmação
- Mudanças no projeto

## Passos
1. `dotnet workload list`
2. Verificar `ANDROID_SDK_ROOT`, `ANDROID_HOME`, `JAVA_HOME`.
3. Verificar pastas padrão do Android SDK.
4. `dotnet build .\HelloWorld\HelloWorld.csproj -f net10.0-android -c Debug` (capturar saída).

## Critérios de aceitação
- Diagnóstico completo com logs capturados.

## Evidências/Logs
- Saída dos comandos acima.

## O que foi feito
- Atualizado para incluir a seção conforme diretriz.

