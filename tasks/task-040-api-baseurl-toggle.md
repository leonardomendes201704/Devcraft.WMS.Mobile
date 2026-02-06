# Tarefa 040: Alternar URL da API entre emulador e dispositivo

## Controle de tempo
- Início: 2026-02-05 20:46
- Fim: 2026-02-05 20:49
- Duração: 00:03

## Objetivo
Permitir alternar a URL base da API entre emulador e dispositivo físico via chave/configuração.

## Contexto
- Usuário quer uma chave para usar uma ou outra URL na geração do APK.

## Escopo
- Criar configuração simples no app.
- Ajustar ApiClient para ler a configuração.
- Expor toggle na tela de login.

## Fora do escopo
- Persistência remota da configuração.

## Passos
1. Adicionar configuração local (Preferences).
2. Ajustar ApiClient para usar a base URL definida.
3. Adicionar toggle na tela de login.

## Critérios de aceitação
- Usuário consegue alternar URL no app.

## Evidências/Logs
- URL selecionada refletida nas chamadas.

## O que foi feito\r\n- `DevcraftWMS.Mobile/AppSettings.cs`: criada configuração persistente para alternar URL (emulador vs dispositivo).\r\n- `DevcraftWMS.Mobile/ApiClient.cs`: usa a URL configurada para Android quando ativada.\r\n- `DevcraftWMS.Mobile/LoginPage.xaml` e `DevcraftWMS.Mobile/LoginPage.xaml.cs`: adicionados toggle e label para alternância.\r\n- Build Android validado (net10.0-android).\r\n

