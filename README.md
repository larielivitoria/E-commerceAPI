# 🛒E-commerce API

O projeto segue boas práticas de arquitetura (DDD) e contém regras de negócio aplicadas em Clientes, Produtos e Pedidos.

Foi construído com foco em estudo, portfólio e evolução profissional, mas já possui estrutura escalável para cenários reais.

Todos os métodos da API são **assíncronos**, usando async/Task, garantindo **alta performance e escalabilidade.**

## 🏗️ Arquitetura

O projeto segue uma arquitetura baseada em camadas:

**Domain** → Entidades, enums e contratos.

**Application** → Services, DTOs e regras de negócio.

**Infrastructure** → Repositórios, DbContext e acesso ao banco.

**API** → Controllers e configuração do projeto.

## ⚠️ Regras de Negócio Implementadas
### Cliente
 - Nome e E-mail não podem estar vazio.
 - Endereço vazio recebe a mensagem: Endereço não informado.
 - Não pode ter E-mail duplicado.
 - E-mail deve ser único.
 - Não apagar cliente se houver pedidos.
 - Validar o Id

### Produto
 - Nome não pode estar vazio.
 - Descrição recebe mensagem se estiver vazia.
 - Preço não pode ser zero.
 - Estoque não pode ser zero.
 - Validar o Id.

### Pedido
 - Não pode atualizar o pedido se já estiver cancelado.
 - Status segue uma ordem lógica.
 - Cliente deve ser válido.
 - Id e quantidade não pode ser zero.
 - Verificar se o produto existe.
 - Verificar estoque disponível.
 - Validar o Id.

## ⚙️ Tecnologias Utilizadas

 - **.NET 9**

 - **Entity Framework Core**

 - **SQL Server**

 - **Swagger (Swashbuckle)**

 - **Dependency Injection**

 - **DTOs e Services**

 - **Tratamento de Exceptions**

 - **Métodos assíncronos (async/Task)**
