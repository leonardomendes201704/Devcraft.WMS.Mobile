# Tarefa 043: Gerar APK arm64-v8a

## Controle de tempo
- Início: 2026-02-05 21:04
- Fim: 2026-02-05 21:04
- Duração: 00:00

## Objetivo
Gerar APK apenas para arm64-v8a para instalação no Galaxy S10.

## Contexto
- Usuário não consegue conectar o celular via cabo.
- APK multi-ABI apresentou erro de análise.

## Escopo
- Publicar APK com RuntimeIdentifier arm64.

## Fora do escopo
- Assinatura para Play Store.

## Passos
1. Publicar com `RuntimeIdentifier=android-arm64`.
2. Informar caminho do APK gerado.

## Critérios de aceitação
- APK arm64-v8a gerado.

## Evidências/Logs
- Saída do publish.

## O que foi feito\r\n- Publish arm64 executado com `RuntimeIdentifier=android-arm64` e `AndroidCreatePackagePerAbi=true`.\r\n- APK arm64 gerado em `DevcraftWMS.Mobile\\bin\\Release\\net10.0-android\\android-arm64\\publish\\com.devcraft.wms.mobile-Signed.apk`.\r\n


