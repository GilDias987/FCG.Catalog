## 🎮 CatalogAPI - FIAP Cloud Games
Este repositório contém o microsserviço de **Catálogo**, parte integrante da arquitetura de microsserviços do projeto FIAP Cloud Games (FCG). O objetivo deste serviço é gerenciar o acervo de jogos e orquestrar o início do fluxo de compra.

## 🚀 Sobre o Microsserviço
O **CatalogAPI** é responsável pelo gerenciamento de jogos e pela interação com a biblioteca do usuário. Ele atua como o ponto de partida para transações comerciais na plataforma, comunicando-se de forma assíncrona com outros serviços através de mensageria.

### Funcionalidades

- **CRUD de Jogos**: Cadastro, leitura, atualização e exclusão de títulos no catálogo.
- **Início de Compra**: Recebe solicitações de compra e inicia o processo via eventos.
- **Gestão de Biblioteca**: Adiciona jogos ao perfil do usuário após a confirmação de pagamento.

## 🏗️ Arquitetura Orientada a Eventos
Este serviço utiliza comunicação assíncrona para garantir a resiliência e o desacoplamento do sistema.

### Eventos Publicados
- `OrderPlacedEvent`: Publicado quando um usuário solicita a compra de um jogo. Contém  `UserId`, `GameId` e `Price`.

### Eventos Consumidos
- `PaymentProcessedEvent`: Consumido para validar se o pagamento foi aprovado.
    - **Se Approved**: O jogo é adicionado à biblioteca do usuário.
    - **Se Rejected**: A transação é cancelada e o status atualizado.

## 🛠️ Tecnologias Utilizadas
- .NET 10 (ASP.NET Core)
- **Entity Framework Core** (Persistência de dados)
- **RabbitMQ/Kafka** (Mensageria via MassTransit ou Client oficial)
- **Docker** (Containerização com Multi-stage build)
- **Kubernetes** (Orquestração de pods e serviços)

## ⚙️ Configurações e Variáveis de Ambiente
As configurações são gerenciadas via `appsettings.json`, mas devem ser sobrescritas por **ConfigMaps** e **Secrets** no ambiente Kubernetes.

Este projeto utiliza variáveis de ambiente para gerenciar configurações específicas de cada ambiente. Para rodar o projeto, você precisará criar um arquivo `.env` na raiz do projeto com as seguintes variáveis:

| Variável | Descrição | Exemplo | Obrigatório |
| :--- | :--- | :--- | :--- |
| `DATABASE_URL` | String de conexão com o Banco de Dados | `mongodb://localhost:27017/meu_banco` | Sim |
| `API_KEY` | Chave de API para serviço externo | `sk_test_12345` | Sim |
| `NODE_ENV` | Ambiente de execução (development, production) | `development` | Sim |
| `NODE_ENV` | Ambiente de execução (development, production) | `development` | Sim |

**Exemplo de arquivo `.env`:**

## 📦 Como Executar

### Via Docker Compose

Para rodar este serviço isoladamente (junto com suas dependências de infraestrutura), utilize o comando na raiz do projeto:

```bash
  docker-compose up -d
```

### Via Kubernetes

```bash
  kubectl apply -f ./k8s/deployment.yaml
  kubectl apply -f ./k8s/service.yaml
  kubectl apply -f ./k8s/configmap.yaml
  kubectl apply -f ./k8s/secret.yaml
```
## 📂 Estrutura de Pastas
```
/
├── src/                # Código fonte da API
├── k8s/                # Manifestos Kubernetes (Deployment, Service, etc)
├── CatalogAPI.sln      # Solution do .NET
└── Dockerfile          # Build otimizado multi-stage
```

## 👥 Integrantes
- **Nome do Grupo:**: 123.
    - **Participantes:**: 
      - Nome (Username Discord).
      - Nome (Username Discord).
      - Nome (Username Discord).
