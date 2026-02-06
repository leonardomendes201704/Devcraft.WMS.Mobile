# Tarefa 049: Implementar execução de picking (TASK-0103)

## Controle de tempo
- Início: 2026-02-06 06:45
- Fim: 2026-02-06 06:49
- Duração: 00:04

## Objetivo
Implementar a execução da separação com scan e confirmação de quantidades.

## Contexto
- TASK-0103 descreve fluxo de execução com validações por localização, produto e lote.

## Escopo
- Tela de execução com item atual, scan e confirmação.
- Integração com GET /api/picking-tasks/{id}.
- Integração com POST /api/picking-tasks/{id}/confirm.

## Fora do escopo
- Ajustes de layout globais.

## Passos
1. Mapear contratos e payloads da API.
2. Criar UI e fluxo de execução.
3. Enviar confirmação e finalizar tarefa.

## Critérios de aceitação
- Operador consegue executar itens e concluir tarefa.
- Validações por scan e parcial funcionam.

## Evidências/Logs
- A preencher.

## O que foi feito
- Criada tela `PickingExecutionPage` com item atual, campos de scan e confirmação.
- Implementado carregamento via GET `/api/picking-tasks/{id}` com header `X-Customer-Id`.
- Adicionada validação de localização, SKU e lote (quando aplicável) e regra de parcial com motivo.
- Envio de confirmação via POST `/api/picking-tasks/{id}/confirm` com itens e quantidades.
- Navegação da fila para a execução usando rota no `AppShell`.
