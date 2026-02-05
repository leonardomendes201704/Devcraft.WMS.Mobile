# Tarefa 012: Habilitar acesso do app móvel à API local

## Controle de tempo
- Início: 2026-02-05 12:15
- Fim: Pendente (após commit e push)
- Duração: Pendente (HH:MM)

## Objetivo
Garantir que o app no Android Emulator consiga acessar a API rodando na máquina host.

## Contexto
- Emulador Android em uso.
- API disponível em `https://localhost:7263/swagger/index.html`.

## Escopo
- Ajustar configuração da API para aceitar conexões externas.
- Ajustar URL base no app para `10.0.2.2`.
- Tratar HTTPS/certificado de desenvolvimento.

## Fora do escopo
- Publicação/produção.

## Passos
1. Localizar projeto da API.
2. Garantir que o servidor escute em `0.0.0.0`/`http://0.0.0.0:porta`.
3. Ajustar URL base do app para `https://10.0.2.2:7263` (ou `http://10.0.2.2:<porta>`).
4. Resolver certificado HTTPS no emulador (confiar cert) ou habilitar HTTP para dev.

## Critérios de aceitação
- App acessa endpoint da API no emulador.

## Evidências/Logs
- Log/print do acesso bem-sucedido.
