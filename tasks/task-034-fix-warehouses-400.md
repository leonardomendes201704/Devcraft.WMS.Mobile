# Tarefa 034: Corrigir erro 400 ao carregar warehouses (contexto cliente)

## Controle de tempo
- Início: 2026-02-05 20:19
- Fim: 2026-02-05 20:22
- Duração: 00:03

## Objetivo
Resolver 400 no endpoint de warehouses adicionando o header de contexto do cliente.

## Contexto
- `api/warehouses` exige `X-Customer-Id` via CustomerContextMiddleware.
- Customers carregam, warehouses retornam 400.

## Escopo
- Adicionar header `X-Customer-Id` ao request de warehouses.
- Recarregar warehouses ao mudar cliente.

## Fora do escopo
- Alterar API.

## Passos
1. Permitir headers custom no ApiClient.
2. Enviar `X-Customer-Id` ao listar warehouses.
3. Recarregar warehouses ao trocar cliente.

## Critérios de aceitação
- Warehouses carregam sem 400.

## Evidências/Logs
- Lista de warehouses carregada no app.

## O que foi feito\r\n- `DevcraftWMS.Mobile/ApiClient.cs`: adicionado suporte a headers customizados por request.\r\n- `DevcraftWMS.Mobile/ContextSelectionPage.xaml.cs`: envio do header `X-Customer-Id` ao carregar warehouses e recarga ao trocar cliente.\r\n- Build Android validado (net10.0-android).\r\n

