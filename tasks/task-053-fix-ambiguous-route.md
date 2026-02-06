# Tarefa 053: Corrigir erro de rota ambígua no picking

## Controle de tempo
- Início: 2026-02-06 08:20
- Fim: 2026-02-06 08:21
- Duração: 00:01

## Objetivo
Eliminar erro de rota ambígua ao finalizar execução de picking.

## Contexto
- App mostrou erro de rotas duplicadas ao finalizar a tarefa.

## Escopo
- Ajuste de navegação para evitar ambiguidade.

## Fora do escopo
- Alterações de layout.

## Passos
1. Identificar fluxo de navegação ao finalizar.
2. Trocar navegação para Shell relativa.

## Critérios de aceitação
- Finalizar a execução sem erro de rota.

## Evidências/Logs
- A preencher.

## O que foi feito
- Ajustada navegação de finalização para `Shell.Current.GoToAsync(\"..\")` evitando rotas ambíguas.
