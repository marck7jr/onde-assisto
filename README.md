# Projeto: [OndeAssisto](https://onde-assisto.azurewebsites.net)
###### Visão do Projeto
###### Versão do Documento 1.0



## Histórico de Revisão

| Descrição | Data | Autor |
|--|--|--|
| Esboço inicial, conversa entre membros e delegação de tarefas | 15/10/2019 | Bruna Hatakeyama, Glauber de Lima Caso, Luiz Rondon, Luiz Tanaka, Miguel Araújo |
| Entrega do Documento de Visão | 22/10/2019 | Bruna Hatakeyama, Glauber de Lima Caso, Luiz Rondon, Luiz Tanaka, Miguel Araújo |

## Índice

Será um site de entretenimento de buscas de filmes e séries	
- #### [Introdução](#Introdução)
- #### [Escopo](#Escopo)
- #### [Definições e Abreviaturas](#Definições-e-Abreviaturas)
- #### [Posicionamento](#Posicionamento)
- #### [Benefícios do Projeto](#Benefícios-do-Projeto)
- #### [Descrição do Problema](#Descrição-do-Problema)
- #### [Descrição dos Usuários](#Descrição-dos-Usuários)
- #### [Usuários Envolvidos no Projeto](#Usuários-Envolvidos-no-Projeto)
- #### [Ambiente do Usuário](#Ambiente-do-Usuário)
- #### [Priorização das Necessidades](#Priorização-das-Necessidades)
- #### [Visão Geral do Produto](#Visão-Geral-do-Produto)
- #### [Perspectiva do Produto](#Perspectiva-do-Produto)
- #### [Licença e Instalação](#Licença-e-Instalação)
- #### [Características Funcionais](#Características-Funcionais)
- #### [Requisitos Funcionais](#Requisitos-Funcionais)
- #### [Requisitos não funcionais](#Requisitos-não-funcionais)
- #### [Aprovação](#Aprovação)

## Introdução
O objetivo deste documento é a coleta, análise e definição das necessidades e características do projeto **OndeAssisto**. Esse sistema visa buscar as plataformas onde determinados filmes ou séries estão disponíveis para serem assistidos, identificando as necessidades dos stakeholders e o detalhamento de como a plataforma irá suprir essas necessidades.
## Escopo
O projeto **OndeAssisto** visa a busca otimizada de obras cinematográficas em formato de filmes e seriados que estão disponíveis em diversas plataformas tecnológicas. De acordo com a requisição do usuário, o sistema retornará: 1) capa/foto, detalhes técnicos e sinopse da obra; 2) canal, data e horário de transmissão; 3) custo financeiro de acesso ao serviço; 4) link para o trailer; 5) histórico de exibição. 
A utilização do sistema possibilitará o consumo de produtos audiovisuais de entretenimento classificados por gênero, atores, plataforma tecnológica, lançamentos, avaliação e custo. As informações serão acessadas na base de obras cadastradas inicialmente pelos desenvolvedores, sendo oportunizado o cadastramento de novos títulos, mediante verificação e validação dos usuários e administradores. 
## Definições e Abreviaturas
Título: Qualquer obra que se apresenta sob a forma de filme ou série.
Api: é um conjunto de rotinas e padrões de programação para acesso a um aplicativo de software ou plataforma baseado na Web. A sigla API refere-se ao termo em inglês "Application Programming Interface" que significa em tradução para o português "Interface de Programação de Aplicativos".

## Posicionamento
### Benefícios do Projeto
O projeto **OndeAssisto** propõe um novo formato de busca por entretenimento no ciberespaço, vez que possibilita o planejamento de uma programação de filmes e seriados que serão acessados no momento mais adequado e oportuno. Gerando assim, não só a otimização de tempo, mas também o desenvolvimento recreativo do usuário.
### Descrição do Problema
Com o aumento da velocidade da internet, os prestadores de serviços de comunicação, se viram obrigados a acompanhar as transformações de uma sociedade cada vez mais exigente, consumista de informações e movida pela influência midiática. 
Nesse cenário cada vez mais digital, surgiram inúmeras plataformas tecnológicas que oferecem entretenimento com vasto portfólio, atendendo todos os tipos de público, contudo a grande demanda oferecida, acaba por dificultar a localização de uma obra audiovisual em exibição.
Dito isto a nossa plataforma irá possibilitar que o usuário busque por um título que deseja assistir e veja de forma centralizada todos os serviços disponíveis para tal fim.

## Descrição dos Usuários

### Usuários Envolvidos no Projeto 
| Papel na Organização | Entidade Representada / Descrição das Atividades | Papel no Projeto / Papel que o representa no Projeto |
| -- | -- | -- |
| Demandante | Cliente ( Idealização da proposta, apreciação das fases do projeto bem como a validação) | Validação |
| Gestor | Coordenar o desenvolvimento do projeto. | Gerência |
| Desenvolvedores | APSII ( Desenvolvimento da solução proposta ) | Desenvolvimento | 
|Usuário final | Usuário ( Consumir colaborar o produto final ) | Preenchimento de plataformas e preços das obras cadastradas |

### Ambiente do Usuário
| Característica | Ambiente Atual | Ambiente Proposto |
| -- | -- | -- |
| Meios de buscar informações sobre obras audiovisual. | Redes sociais, chats, grades de programação dos prestadores de serviços existentes. | Aplicação web centralizando todas estas informações. |

### Priorização das Necessidades
| Descrição da Necessidade (Problema) | Solução Atual | Solução Proposta |
| -- | -- | -- |
| Fonte de dados para preenchimento de obras | Api’s externas | Api’s externas |
| Validação das informações preenchidas | Votação dos usuários e validação por um Administrador | Validação final do administrador do sistema |

### Visão Geral do Produto
O foco principal do produto é possibilitar que o usuário saiba quando, onde e por quanto, determinado filme ou série de interesse estará disponível para assistir, essas informações serão atualizadas com a colaboração de todos os usuários, os quais receberão feedbacks dos mesmos, podendo ou não ser validados por administradores.
#### Perspectiva do Produto

![Items.png](/.attachments/Items-f5b0139b-db8c-467c-84f7-fb28f6f6acf0.png)

### Licença e Instalação
Aplicação web de acesso público.

### Características Funcionais
Esta seção define e descreve as características funcionais do **OndeAssisto**. As características funcionais são capacidades necessárias ao sistema para que o mesmo possa atender a demanda funcional do usuário.
#### 1. Busca pelas mídias de interesse; 
- Autor;
- Atores;
- Gênero;
- Plataforma;
- Ano de lançamento
#### 2. Listagem dos títulos cadastrados:
- Dados: Título, Imagem/Capa, Sinopse, link para um trailer
- Detalhes da plataforma onde o filme e/ou série está disponível, incluindo o custo de exibição;
-  Data prevista do término da disponibilidade por plataforma;
#### 3. Colaboração de usuário para preencher os dados;
- Plataforma disponível;
- Data limite;
- Validação dos usuários
#### 4. Autenticação de usuários;
 - Comum;
 - Administrador;
#### 5. Preenchimento manual de novos títulos por um administrador responsável;
#### 6. Histórico de exibição das obras ex: “em 2018 filme x estava no cinema”;
#### 7. Filmes/Séries em destaque filmes;
#### 8. Notificação de lançamento e/ou disponibilidade de filmes e séries por e-mail, informando a plataforma, horários e custo;
#### 9. Últimas atualizações com validação dos usuários;
#### 10. Escolher filmes de interesse e ver todos de uma vez;

### Requisitos Funcionais
- [RF01] CADASTRAR USUÁRIO
- [RF02] ACESSAR CONTA
- [RF03] BUSCAR OBRAS DISPONÍVEIS
- [RF04] COMPLETAR INFORMAÇÕES DA OBRA
- [RF05] CONSULTAR HISTÓRICO DE EXIBIÇÕES
- [RF06] SUGERIR NOVO FILME OU SÉRIE
- [RF07] CLASSIFICAR INFORMAÇÕES DA OBRA
- [RF08] MARCAR OBRA COMO FAVORITA
- [RF09] EXIBIR CONTRIBUIÇÕES
- [RF10] INSERIR NOVA OBRA
- [RF11] LISTAR OBRAS DISPONÍVEIS
- [RF12] EDITAR INFORMAÇÕES DA OBRA
- [RF13] REMOVER OBRA

### Requisitos não funcionais
- As páginas deve carregar em até no máximo 3 segundos;
- A busca por título deve levar até 1 segundo;
- As informações não devem ser atualizadas constantemente, no máximo 1 mês;
- Os dados devem ser mantidos em segurança livre de invasões e alterações indevidas;
- Usuários com dificuldades ou deficiências motoras ou visuais devem conseguir usar a plataforma com facilidade.
### Aprovação
| Autor: | <Nome> |
| -- | -- |

| Data | Função | Nome | Assinatura |
| -- | -- | -- | -- |
| 22.10.2019 | | | |
