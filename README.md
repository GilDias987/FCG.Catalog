# 🎮 FIAP Cloud Games - CatalogAPI

Responsável pelo gerenciamento do acervo de jogos, biblioteca dos usuários e pela orquestração inicial do fluxo de compra.

## 1. Funcionalidades
* CRUD completo de jogos.
* Gerenciamento da biblioteca de jogos por usuário.
* Início do fluxo de compra de um jogo.

## 2. Fluxo Orientado a Eventos
Este serviço é o ponto central da orquestração de pedidos.

* **Publicados:**
    * `OrderPlacedEvent`: Publicado quando um usuário solicita a compra de um jogo (contém UserId, GameId e Price).
* **Consumidos:**
    * `PaymentProcessedEvent`: Consumido para validar a transação. Se o status for `Approved`, o jogo é oficialmente adicionado à biblioteca do usuário.

## 3. Tecnologias
* **Linguagem:** .NET 10
* **Banco de Dados:** SQL Server
* **Mensageria:** RabbitMQ (via MassTransit)
* **Padrões:** MediatR, FluentValidation
* **Documentação:** Swagger
* **Orquestração:** Docker & Kubernetes

## 4. Variáveis de Ambiente
| Variável | Descrição | Exemplo |
| :--- | :--- | :--- |
| `ConnectionStrings__DefaultConnection` | String de conexão com SQL Server | `Server=db;Database=CatalogDb;...` |
| `RabbitMQ__Host` | Host do Broker de Mensageria | `rabbitmq://rabbitmq-service` |
| `PaymentsApi__Url` | URL base do serviço de pagamentos | `http://payments-api:80` |

## 👥 Integrantes
- **Nome do Grupo:**: 33.
    - **Participantes:**: 
      - Alexandre Araújo da Silva (AlexandreAraujo).
      - Josegil Dias Frota Figueira (gildiasfrota).
      - Miguel de Oliveira Gonçalves (miguel084).

