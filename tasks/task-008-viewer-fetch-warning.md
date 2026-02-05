# Tarefa 008: Aviso de fetch bloqueado e instruções de servidor local

## Controle de tempo
- Início: 2026-02-05 12:00
- Fim: 2026-02-05 12:08
- Duração: 00:08

## Objetivo
Exibir aviso quando o carregamento automático falhar e incluir instruções para subir um servidor local simples.

## Contexto
- Usuário pediu aviso e script simples para abrir servidor local.

## Escopo
- Atualizar `tasks/tasks-viewer.html` com alerta de fallback.
- Incluir instruções rápidas de servidor local no UI.

## Fora do escopo
- Scripts externos ou instalação de dependências.

## Passos
1. Detectar falha de `fetch` no carregamento automático.
2. Exibir alerta com instruções (ex.: `python -m http.server`).

## Critérios de aceitação
- Se o `fetch` falhar, usuário vê instruções claras para abrir servidor local.

## Evidências/Logs
- `tasks/tasks-viewer.html` atualizado.

