# Tarefa 035: Ajustar DTO de warehouses para desserialização

## Controle de tempo
- Início: 2026-02-05 20:26
- Fim: 2026-02-05 20:29
- Duração: 00:03

## Objetivo
Corrigir falha de desserialização do JSON de warehouses no app.

## Contexto
- API retorna `warehouseType` como número (enum).
- DTO atual usa `string` e falha ao converter.

## Escopo
- Ajustar DTO de `WarehouseItem`.

## Fora do escopo
- Alterar API.

## Passos
1. Ajustar tipo de `warehouseType`.
2. Validar carregamento.

## Critérios de aceitação
- Warehouses carregam sem erro de conversão.

## Evidências/Logs
- Lista carregada no app.

## O que foi feito\r\n- `DevcraftWMS.Mobile/ContextSelectionPage.xaml.cs`: alterado `WarehouseItem.WarehouseType` de `string` para `int` para casar com JSON da API.\r\n- Build Android validado (net10.0-android).\r\n

