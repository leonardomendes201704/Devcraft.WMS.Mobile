# Tarefa 047: Corrigir conversao JSON da fila de picking

## Controle de tempo
- Início: 2026-02-05 21:38
- Fim: 2026-02-05 21:39
- Duração: 00:01

## Objetivo
Ajustar o modelo/parse da resposta da API de picking tasks.

## Contexto
- App falha ao desserializar retorno de /api/picking-tasks.

## Escopo
- Corrigir DTOs e conversao do JSON.

## Fora do escopo
- Alteracoes de layout.

## Passos
1. Inspecionar DTO atual no app.
2. Ajustar tipos e propriedades para casar com JSON.
3. Validar carregamento da lista.

## Critérios de aceitação
- Lista carregando sem erro de desserializacao.

## Evidências/Logs
- A preencher.

## O que foi feito
- Ajustado modelo de picking task para aceitar `status` numérico da API.
- Criado `StatusText` derivado para exibição e comparação no app.
- Alterado filtro para enviar `status` numérico no querystring.
- UI passou a bindar `StatusText` no card.
