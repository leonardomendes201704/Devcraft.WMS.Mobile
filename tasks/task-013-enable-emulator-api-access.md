# Tarefa 013: Ajustar API e app para acesso via emulador

## Controle de tempo
- Início: 2026-02-05 12:15
- Fim: Pendente (após commit e push)
- Duração: Pendente (HH:MM)

## Objetivo
Configurar API para aceitar conexões externas e ajustar app MAUI para consumir a API via Android Emulator.

## Contexto
- API em `C:\Leonardo\Labs\DevCraftSolutions\DevcraftWMS\src\DevcraftWMS.Api\DevcraftWMS.Api.csproj`.
- Emulador Android em uso.
- Usuário aceitou uso de HTTP para dev.

## Escopo
- Atualizar `launchSettings.json` para escutar em `0.0.0.0` no HTTP.
- Ajustar app MAUI para usar `http://10.0.2.2:5137` no Android.
- Adicionar UI simples para testar chamada.

## Fora do escopo
- Produção/segurança.

## Passos
1. Atualizar URLs de execução da API.
2. Implementar chamada HTTP no app e exibir resposta.
3. Validar no emulador.

## Critérios de aceitação
- App no emulador consegue acessar a API local.

## Evidências/Logs
- URL utilizada e resultado exibido no app.
