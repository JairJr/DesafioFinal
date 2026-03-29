# [DesafioFinal](https://github.com/JairJr/DesafioFinal)

Desafio final de módulo XPE com o objetivo de aplicar os conhecimentos de arquitetura de software, 
focando na implementação de uma API RESTful, seguindo o padrão MVC. A ideia 
é explorar práticas de design e construção de APIs, documentação de arquitetura 
e organização de código.

## 📋 Métodos Solicitados

▪ **CRUD**: Criação (Create), Leitura (Read), Atualização (Update) e Exclusão (Delete)  
▪ **Contagem**: Endpoint para retornar o número total de registros  
▪ **Find All**: Endpoint para retornar todos os registros  
▪ **Find By ID**: Endpoint para retornar um registro específico com base no ID  
▪ **Find By Name**: Endpoint para retornar registros que correspondam a um nome específico

## 🏗️ Desenho Arquitetural do Software

Este projeto utiliza uma arquitetura em camadas (Layered Architecture), seguindo os princípios de separação de responsabilidades e inversão de dependências. A estrutura está organizada conforme o modelo C4, focando na clareza e manutenibilidade do código.

### Diagrama C4


<img width="811" height="671" alt="Diagrama-C4 N1 - Context drawio" src="https://github.com/user-attachments/assets/b3b2528c-c67a-41c0-87df-f7a3e36bb585" />

<img width="765" height="951" alt="Diagrama-C4 N2 - Container drawio" src="https://github.com/user-attachments/assets/a0066ae6-3543-439f-a61d-7c743d510c31" />

### Camadas da Arquitetura

**1. Controllers (Camada de Apresentação)**
- Responsável por receber as requisições HTTP
- Valida os dados de entrada
- Orquestra as chamadas aos serviços
- Retorna as respostas apropriadas (DTOs)

**2. Services (Camada de Lógica de Negócio)**
- Implementa as regras de negócio da aplicação
- Processa os dados recebidos dos controllers
- Coordena operações entre múltiplos repositórios
- Garante a integridade das operações

**3. Repositories (Camada de Acesso a Dados)**
- Abstrai o acesso ao banco de dados
- Implementa operações CRUD básicas
- Utiliza Entity Framework Core para persistência
- Segue o padrão Repository para desacoplamento

**4. Models (Entidades de Domínio)**
- Define as entidades do domínio
- Representa a estrutura dos dados
- Mapeamento para o banco de dados

**5. DTOs (Data Transfer Objects)**
- Objetos para transferência de dados entre camadas
- Separa a representação interna das entidades da API
- Evita exposição desnecessária de dados

## 📁 Estrutura de Pastas e Componentes

```
OrderApi/
│
├── Controllers/                    # Camada de Apresentação
│   ├── OrdersController.cs        # Endpoints para gerenciamento de pedidos
│   ├── CustomersController.cs     # Endpoints para gerenciamento de clientes
│   └── ProductsController.cs      # Endpoints para gerenciamento de produtos
│
├── Services/                       # Camada de Lógica de Negócio
│   ├── Interfaces/
│   │   └── IOrderService.cs       # Contrato do serviço de pedidos
│   └── OrderService.cs            # Implementação das regras de negócio
│
├── Repositories/                   # Camada de Acesso a Dados
│   ├── Interfaces/
│   │   └── IOrderRepository.cs    # Contrato do repositório
│   └── OrderRepository.cs         # Implementação do acesso a dados
│
├── Models/                         # Entidades de Domínio
│   ├── Order.cs                   # Entidade de Pedido
│   ├── Customer.cs                # Entidade de Cliente
│   └── Product.cs                 # Entidade de Produto
│
├── DTOs/                          # Data Transfer Objects
│   ├── OrderRequestDto.cs         # DTO para requisições de pedidos
│   └── OrderResponseDto.cs        # DTO para respostas de pedidos
│
├── Data/                          # Configuração do Banco de Dados
│   └── AppDbContext.cs            # Contexto do Entity Framework
│
├── DependencyInjection/           # Injeção de Dependências
│   └── Dependencies.cs            # Configuração de DI
│
└── Program.cs                     # Ponto de entrada da aplicação

Tests/
└── OrderApi.Tests/                # Testes Unitários e de Integração
```

### Descrição dos Componentes

#### **Controllers**
Pontos de entrada da API que expõem os endpoints HTTP. Cada controller é responsável por um recurso específico (Orders, Customers, Products) e implementa os métodos solicitados (CRUD, Count, FindAll, FindById, FindByName).

#### **Services**
Contêm a lógica de negócio da aplicação. Isolam as regras de negócio dos controllers e repositórios, facilitando testes e manutenção. Implementam validações complexas e orquestração de operações.

#### **Repositories**
Implementam o padrão Repository para abstrair o acesso ao banco de dados. Utilizam Entity Framework Core e seguem interfaces para permitir substituição e testes com mocks.

#### **Models (Entidades)**
Representam as entidades de domínio da aplicação (Order, Customer, Product). Mapeadas diretamente para tabelas no banco de dados através do Entity Framework Core.

#### **DTOs (Data Transfer Objects)**
Objetos utilizados para transferir dados entre a API e os clientes. Separados das entidades para evitar exposição de dados internos e permitir versionamento da API.

#### **Data**
Contém o `AppDbContext`, que é o contexto do Entity Framework Core responsável por configurar o mapeamento das entidades e gerenciar a conexão com o banco de dados.

#### **DependencyInjection**
Centraliza a configuração de injeção de dependências, registrando serviços, repositórios e outras dependências necessárias para o funcionamento da aplicação.

## 🚀 Tecnologias Utilizadas

- **.NET 9** - Framework principal
- **ASP.NET Core** - Web API
- **Entity Framework Core** - ORM para acesso a dados
- **Swagger** - Documentação da API
- **Dependency Injection** - Gerenciamento de dependências
