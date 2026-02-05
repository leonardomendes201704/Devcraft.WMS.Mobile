# Tarefa 031: Implementar login + seleção de contexto (cliente/armazém)

## Controle de tempo
- Início: 2026-02-05 19:09
- Fim: Pendente (após commit e push)
- Duração: Pendente (HH:MM)

## Objetivo
Implementar fluxo completo: login, carregamento de clientes/armazéns, seleção de contexto e exibição no topo do menu.

## Contexto
- Endpoints estão na API local em `C:\Leonardo\Labs\DevCraftSolutions\DevcraftWMS\src\DevcraftWMS.Api\Controllers`.
- API exige Bearer token.

## Escopo
- Tela de login existente.
- Nova tela de seleção de contexto.
- Persistência de token + contexto.
- Exibir cliente/armazém ativo no topo do menu.

## Fora do escopo
- RBAC, MFA/SSO.

## Passos
1. Identificar contratos nos controllers.
2. Modelar DTOs e serviços.
3. Implementar tela de seleção e navegação.
4. Persistir contexto e exibir no menu.

## Critérios de aceitação
- Login ok → seleção de cliente/armazém → menu.
- Cabeçalho mostra cliente/armazém ativo.
- Requer token nas chamadas.

## Evidências/Logs
- Fluxo funcional no app.

## O que foi feito
- Task criada para implementação do fluxo.
