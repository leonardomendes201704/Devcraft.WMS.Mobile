# Tarefa 040: Alternar URL da API entre emulador e dispositivo

## Controle de tempo
- Início: 2026-02-05 20:46
- Fim: Pendente (após commit e push)
- Duração: Pendente (HH:MM)

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

## O que foi feito
- Task criada para alternância de URL.
