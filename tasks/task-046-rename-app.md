# Tarefa 046: Renomear app para DevcraftWMS.Mobile

## Controle de tempo
- Início: 2026-02-05 21:26
- Fim: 2026-02-05 21:30
- Duração: 00:04

## Objetivo
Atualizar nome do app, namespaces e referências de DevcraftWMS.Mobile para DevcraftWMS.Mobile.

## Contexto
- O app evoluiu e precisa refletir a marca correta em toda a solution.

## Escopo
- Atualizar nomes do projeto/assembly/namespace/app id.
- Substituir referências a DevcraftWMS.Mobile.

## Fora do escopo
- Alterações funcionais além de renome.

## Passos
1. Localizar referências a DevcraftWMS.Mobile.
2. Atualizar nome do projeto/assembly/app id e namespaces.
3. Ajustar arquivos da solution.

## Critérios de aceitação
- Build sem referências a DevcraftWMS.Mobile.
- Nome exibido do app e namespace base atualizados.

## Evidências/Logs
- A preencher.

## O que foi feito
- Pasta do projeto renomeada para `DevcraftWMS.Mobile` e csproj para `DevcraftWMS.Mobile.csproj`.
- Atualizados namespaces e `x:Class` em XAML/CS para `DevcraftWMS.Mobile`.
- Ajustados identificadores do app (RootNamespace, AssemblyName, ApplicationTitle e ApplicationId).
- Corrigidas referências no `.slnx` e nomes de manifest WinUI.
- Atualizadas referências nas tasks antigas para o novo caminho.

