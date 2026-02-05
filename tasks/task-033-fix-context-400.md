# Tarefa 033: Corrigir erro 400 na seleção de contexto

## Controle de tempo
- Início: 2026-02-05 19:55
- Fim: Pendente (após commit e push)
- Duração: Pendente (HH:MM)

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

## O que foi feito
- Task criada para corrigir erro 400.
