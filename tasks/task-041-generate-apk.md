# Tarefa 041: Gerar APK para dispositivo físico

## Controle de tempo
- Início: 2026-02-05 20:51
- Fim: 2026-02-05 20:46
- Duração: 00:-05

## Objetivo
Gerar APK Release para instalação no dispositivo físico.

## Contexto
- Usuário solicitou APK para testar no celular.

## Escopo
- Executar `dotnet publish` com formato APK.
- Informar caminho do APK.

## Fora do escopo
- Assinatura para Play Store.

## Passos
1. Executar publish Release para net10.0-android.
2. Localizar APK gerado.

## Critérios de aceitação
- APK gerado em `bin/Release/.../publish`.

## Evidências/Logs
- Saída do publish.

## O que foi feito\r\n- Build Release APK executado com sucesso (net10.0-android).\r\n- APK gerado em `DevcraftWMS.Mobile\\bin\\Release\\net10.0-android\\publish\\` (nome `com.devcraft.wms.mobile-Signed.apk`).\r\n- Processo travado por lock foi resolvido encerrando processos dotnet/MSBuild antes do publish.\r\n


