# Tarefa 021: Persistir token de autenticação no app

## Controle de tempo
- Início: 2026-02-05 12:39
- Fim: 2026-02-05 12:41
- Duração: 00:02

## Objetivo
Persistir o token JWT retornado pelo login para uso posterior no app.

## Contexto
- Login retorna `token`, mas ainda não está persistido.

## Escopo
- Salvar token no SecureStorage.
- Disponibilizar leitura do token quando necessário.

## Fora do escopo
- Refresh token.

## Passos
1. Persistir token ao efetuar login.
2. Expor método utilitário para recuperar token.

## Critérios de aceitação
- Token salvo e disponível após reiniciar o app.

## Evidências/Logs
- Token salvo com sucesso.

## O que foi feito
- Task criada para persistência do token.

