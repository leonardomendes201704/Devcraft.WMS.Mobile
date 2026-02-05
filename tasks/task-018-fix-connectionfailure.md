# Tarefa 018: Diagnosticar ConnectionFailure no app ao acessar API local

## Controle de tempo
- Início: 2026-02-05 12:27
- Fim: 2026-02-05 12:30
- Duração: 00:03

## Objetivo
Resolver falha de conexão do app MAUI ao acessar API local pelo Android Emulator.

## Contexto
- API sobe e abre `http://localhost:5137/swagger/index.html` no host.
- App móvel retorna `ConnectionFailure`.
- App usa `http://10.0.2.2:5137` no Android.

## Escopo
- Validar bind/escuta da API em `0.0.0.0`.
- Validar acesso ao endpoint via emulador.
- Validar regras de firewall/antivírus.

## Fora do escopo
- Produção/HTTPS.

## Passos
1. Confirmar `applicationUrl` com `0.0.0.0:5137`.
2. Testar no emulador: abrir `http://10.0.2.2:5137/swagger/index.html`.
3. Verificar firewall liberando a porta 5137.
4. Ajustar app/endpoint se necessário.

## Critérios de aceitação
- App consegue acessar a API sem `ConnectionFailure`.

## Evidências/Logs
- Resultado do teste no emulador.

## O que foi feito
- Confirmado acesso via navegador do emulador em http://10.0.2.2:5137.

