# Tarefa 033: Corrigir erro 400 na seleção de contexto

## Controle de tempo
- Início: 2026-02-05 19:55
- Fim: 2026-02-05 20:13
- Duração: 00:18

## Objetivo
Resolver o erro 400 BadRequest ao carregar clientes/armazéns na tela de seleção de contexto.

## Contexto
- Erro 400 na tela de seleção de contexto.
- Endpoints `api/customers` e `api/warehouses` exigem autorização e parâmetros válidos.

## Escopo
- Ajustar chamadas e parâmetros.
- Melhorar feedback de erro.

## Fora do escopo
- Alterar API.

## Passos
1. Capturar mensagem do 400.
2. Ajustar query/contrato.
3. Revalidar no app.

## Critérios de aceitação
- Tela carrega lista de clientes/armazéns sem erro 400.

## Evidências/Logs
- Resultado da chamada e lista carregada.

## O que foi feito\r\n- Identificado limite de `pageSize` (máx 100) nos validators da API, causando 400.\r\n- `DevcraftWMS.Mobile/ContextSelectionPage.xaml.cs`: reduzido `pageSize` de 200 para 100 nas chamadas de customers/warehouses.\r\n- Build Android validado com sucesso (net10.0-android).\r\n

