# Tarefa 032: Corrigir erro de build e warnings

## Controle de tempo
- Início: 2026-02-05 19:36
- Fim: Pendente (após commit e push)
- Duração: Pendente (HH:MM)

## Objetivo
Corrigir erro de build e ajustar warnings de métodos obsoletos.

## Contexto
- Erro: `JsonContent` não encontrado em `ApiClient.cs`.
- Warnings: `ScaleTo`/`RotateTo` obsoletos em `WelcomePage.xaml.cs`.

## Escopo
- Ajustar imports/uso do `JsonContent`.
- Trocar para `ScaleToAsync`/`RotateToAsync`.

## Fora do escopo
- Refatorações adicionais.

## Passos
1. Corrigir `ApiClient.cs`.
2. Ajustar animações no `WelcomePage.xaml.cs`.
3. Rebuild.

## Critérios de aceitação
- Build sem erros.

## Evidências/Logs
- Saída do build.

## O que foi feito
- Task criada para corrigir build.
