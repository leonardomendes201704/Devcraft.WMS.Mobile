# Tarefa 019: Permitir HTTP no Android para API local

## Controle de tempo
- Início: 2026-02-05 12:30
- Fim: 2026-02-05 12:31
- Duração: 00:01

## Objetivo
Habilitar tráfego HTTP (cleartext) no app Android para evitar `ConnectionFailure` ao acessar a API local.

## Contexto
- Emulador acessa `http://10.0.2.2:5137/swagger/index.html` via browser.
- App retorna `ConnectionFailure` ao usar HTTP.

## Escopo
- Permitir cleartext no Android manifest.

## Fora do escopo
- HTTPS com certificado confiável.

## Passos
1. Ajustar `AndroidManifest.xml` para permitir cleartext.
2. Rebuild do app no emulador.

## Critérios de aceitação
- App consegue acessar a API via HTTP sem erro de conexão.

## Evidências/Logs
- Status HTTP exibido no app.

## O que foi feito
- Permitido HTTP no Android Manifest para evitar ConnectionFailure.

