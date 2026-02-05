# Tarefa 035: Ajustar DTO de warehouses para desserialização

## Controle de tempo
- Início: 2026-02-05 20:26
- Fim: Pendente (após commit e push)
- Duração: Pendente (HH:MM)

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

## O que foi feito
- Task criada para corrigir desserialização.
