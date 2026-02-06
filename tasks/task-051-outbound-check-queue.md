# Tarefa 051: Implementar fila de conferência outbound (TASK-0104)

## Controle de tempo
- Início: 2026-02-06 06:55
- Fim: 2026-02-06 07:42
- Duração: 00:47

## Objetivo
Implementar a fila de conferência outbound com filtros e ação de start.

## Contexto
- TASK-0104 descreve listagem e início de tarefas de conferência.

## Escopo
- Tela de fila e filtros.
- Integração GET /api/outbound-checks.
- Integração POST /api/outbound-checks/{id}/start.

## Fora do escopo
- Execução detalhada da conferência.

## Passos
1. Mapear contrato da API.
2. Criar UI da fila.
3. Implementar start e navegação.

## Critérios de aceitação
- Listar e iniciar tarefas de conferência.

## Evidências/Logs
- A preencher.

## O que foi feito
- Criada tela `OutboundCheckQueuePage` com filtros (status/prioridade), busca por OS e listagem paginada.
- Implementada chamada GET `/api/outbound-checks` com `X-Customer-Id` e filtro por warehouse atual.
- Adicionada ação Start com POST `/api/outbound-checks/{id}/start` e navegação para execução.
- Criada tela `OutboundCheckExecutionPage` para exibir resumo e itens retornados no start.
- Menu “Conferência” agora navega para a fila de conferência.
