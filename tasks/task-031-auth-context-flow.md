# Tarefa 031: Implementar login + seleção de contexto (cliente/armazém)

## Controle de tempo
- Início: 2026-02-05 19:09
- Fim: 2026-02-05 19:15
- Duração: 00:06

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

## O que foi feito\r\n- `HelloWorld/ApiClient.cs`: criado cliente HTTP com Bearer token e parsing JSON para consumo de endpoints.\r\n- `HelloWorld/AuthStorage.cs`: adicionados campos de contexto (customer/warehouse) com persistência no SecureStorage.\r\n- `HelloWorld/ContextSelectionPage.xaml`: criada UI de seleção de cliente e armazém.\r\n- `HelloWorld/ContextSelectionPage.xaml.cs`: carregamento de `api/customers` e `api/warehouses` (PagedResult) e gravação do contexto.\r\n- `HelloWorld/LoginPage.xaml.cs`: após login, navega para seleção de contexto (ou menu se já houver contexto).\r\n- `HelloWorld/WelcomePage.xaml` e `HelloWorld/WelcomePage.xaml.cs`: exibem cliente/armazém ativo no topo do menu.\r\n- `HelloWorld/AppShell.xaml` e `HelloWorld/AppShell.xaml.cs`: rotas adicionadas para seleção de contexto e menu.\r\n
