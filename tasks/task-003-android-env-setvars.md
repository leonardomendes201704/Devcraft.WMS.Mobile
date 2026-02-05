# Tarefa 003: Configurar variáveis de ambiente Android/JDK

## Controle de tempo
- Início: 2026-02-05 11:52
- Fim: 2026-02-05 12:09
- Duração: 00:17


## Objetivo
Definir `ANDROID_SDK_ROOT`, `ANDROID_HOME` e `JAVA_HOME` para que o Visual Studio reconheça o ambiente Android.

## Contexto
- Android SDK encontrado em `C:\Program Files (x86)\Android\android-sdk`.
- Variáveis de ambiente estão vazias.
- Build via CLI funciona, mas Visual Studio acusa ambiente não configurado.

## Escopo
- Configurar variáveis no nível de máquina.
- Validar valores após setar.

## Fora do escopo
- Instalação de SDK/JDK adicionais.

## Passos
1. Detectar JDK instalado.
2. Definir `ANDROID_SDK_ROOT` e `ANDROID_HOME`.
3. Definir `JAVA_HOME`.
4. Confirmar valores.

## Critérios de aceitação
- Variáveis de ambiente persistidas no Windows.
- Visual Studio reconhece Android SDK após reinício.

## Evidências/Logs
- Saída de consulta às variáveis após set.



