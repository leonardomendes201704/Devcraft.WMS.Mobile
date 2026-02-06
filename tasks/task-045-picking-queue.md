# Tarefa 045: Implementar fila de picking (TASK-0102)

## Controle de tempo
- Início: 2026-02-05 21:11
- Fim: 2026-02-05 21:21
- Duração: 00:10

## Objetivo
Implementar a tela de fila de picking com filtros, busca e ação de start.

## Contexto
- TASK-0102 solicitada pelo usuário.

## Escopo
- Tela de fila com filtros e lista.
- Chamada GET /api/picking-tasks.
- Ação POST /api/picking-tasks/{id}/start.

## Fora do escopo
- Detalhes completos da tarefa.

## Passos
1. Verificar contratos dos endpoints na API.
2. Criar tela de fila e bindings.
3. Implementar ação Start com validações.

## Critérios de aceitação
- Lista com filtros e ação de start.
- Status muda para InProgress após start.

## Evidências/Logs
- Tela funcionando no app.

## O que foi feito
- Criada a tela `PickingQueuePage` com busca, filtros (status e ativo) e lista com card de tarefas.
- Implementado binding de dados e carregamento da fila via GET `/api/picking-tasks`, com headers de contexto (X-Customer-Id).
- Adicionada ação "Start" com POST `/api/picking-tasks/{id}/start` e validações básicas de status.
- Ajustado o menu para navegar para a fila de picking (card "Picking (Fila)").
- Registrada rota no `AppShell` para navegação da nova página.
- Criado endpoint de start na API (repo DevcraftWMS) para suportar o fluxo mobile.
