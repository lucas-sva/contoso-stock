<a id="readme-top"></a>

<!-- PROJECT LOGO -->
<br />
<div align="center">
  <a href="https://github.com/lucas-sva/contoso-stock">
    <img src="assets/logo.png" alt="Logo" width="80" height="80">
  </a>

<h3 align="center">Contoso Stock</h3>

  <p align="center">
    Sistema de Alocação Otimizada para Logística B2B de Alta Performance
    <br />
    <a href="https://github.com/lucas-sva/contoso-stock/wiki"><strong>Explore a Wiki »</strong></a>
    <br />
    <br />
  </p>
</div>

<!-- ABOUT THE PROJECT -->
## Sobre o projeto

A **Contoso Stock** é uma plataforma de logística focada em resolver o desafio de *fulfillment* em cadeias de suprimentos complexas. Nela, a margem de lucro depende da **precisão da alocação**. Este sistema foi desenhado para garantir que o produto certo saia do galpão mais eficiente, evitando colisões de inventário e desperdício de frete.

### Por que este projeto existe?
 O projeto **Contoso Stock** (um sistema de logística B2B) é desenvolvido em paralelo ao estudo aprofundado do livro:
**Learning Domain-Driven Design**, de Vladik Khononov  

### Estrutura do Domínio (Strategic Mapping)
De acordo com a análise estratégica:

- **Fulfillment (Core):** Inteligência de reserva e alocação (RAO). Onde reside o diferencial competitivo.
- **Catalog (Supporting):** Gestão de SKUs e especificações técnicas.
- **Identity (Generic):** Autenticação e controle de acesso.

### Built With

* [![.NET][dotnet-shield]][dotnet-url]
* [![Docker][docker-shield]][docker-url]
* [![Dev Container][devcontainer-shield]][devcontainer-url]
* [![GitHub Actions][gha-shield]][gha-url]

<br />

<!-- GETTING STARTED -->
## Como começar

Para rodar o projeto localmente e contribuir, siga os passos abaixo. Recomendamos o uso de **Dev Containers** para garantir um ambiente idêntico ao de produção.

### Pré-requisitos

* **Docker Desktop**
* **VS Code** com a extensão **Dev Containers** instalada.

### Instalação

1. Clone o repositório:
   ```sh
   git clone https://github.com/lucas-sva/contoso-stock.git
    ```

2. Abra a pasta no VS Code.
3. Quando solicitado, clique em **"Reopen in Container"**.
4. O ambiente será configurado automaticamente com o .NET 10 SDK.
5. Compile o projeto:
    ```sh
    dotnet build
    ```
<br />

<!-- ROADMAP -->
## Roadmap

O projeto será evoluído em ciclos, seguindo os capítulos da obra de Vladik Khononov:

- [x] **Parte I: Análise Estratégica**
    - [x] Cap 1: Analisando Domínios de Negócio (Mapeamento de Subdomínios)
    - [x] Cap 2: Descobrindo Conhecimento de Domínio (Linguagem Ubíqua)
    - [ ] Cap 3: Gerenciando a Complexidade (Bounded Contexts)
    - [ ] Cap 4: Context Mapping
  
<span></spam>

- [ ] **Parte II: Design Tático**
    - [ ] Cap 5 a 8: Agregados, Entidades e Value Objects

<span></spam>

- [ ] **Parte III: Arquitetura**
    - [ ] Cap 9 a 11: Padrões Arquiteturais e Comunicação

<span></spam>

- [ ] **Parte IV: Contexto e Dados**
    - [ ] Cap 12 a 16: Microservices e Modernização
  
Veja as [issues em aberto](https://github.com/lucas-sva/contoso-stock/issues) para uma lista completa de funcionalidades propostas.

<br />

<!-- CONTACT -->
## Contato

Lucas Silva - [LinkedIn](https://www.linkedin.com/in/-lucassva/) - lucas.sva@outlook.com


<p align="right">(<a href="#readme-top">voltar ao topo</a>)</p>



<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-shield]: https://img.shields.io/github/contributors/github_username/repo_name.svg?style=for-the-badge
[contributors-url]: https://github.com/github_username/repo_name/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/github_username/repo_name.svg?style=for-the-badge
[forks-url]: https://github.com/github_username/repo_name/network/members
[stars-shield]: https://img.shields.io/github/stars/github_username/repo_name.svg?style=for-the-badge
[stars-url]: https://github.com/github_username/repo_name/stargazers
[issues-shield]: https://img.shields.io/github/issues/github_username/repo_name.svg?style=for-the-badge
[issues-url]: https://github.com/github_username/repo_name/issues
[license-shield]: https://img.shields.io/github/license/github_username/repo_name.svg?style=for-the-badge
[license-url]: https://github.com/github_username/repo_name/blob/master/LICENSE.txt
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[docker-shield]: https://img.shields.io/badge/Docker-2496ED?style=for-the-badge&logo=docker&logoColor=white
[linkedin-url]: https://linkedin.com/in/linkedin_username
[product-screenshot]: images/screenshot.png
[dotnet-shield]: https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white
[gha-shield]: https://img.shields.io/badge/GitHub%20Actions-2088FF?style=for-the-badge&logo=githubactions&logoColor=white
[devcontainer-shield]: https://img.shields.io/badge/Dev%20Container-2A7BDE?style=for-the-badge&logo=visualstudiocode&logoColor=white
[csharp-shield]: https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white

<!-- Shields.io badges. You can a comprehensive list with many more badges at: https://github.com/inttter/md-badges -->
[Next.js]: https://img.shields.io/badge/next.js-000000?style=for-the-badge&logo=nextdotjs&logoColor=white
[Next-url]: https://nextjs.org/
[React.js]: https://img.shields.io/badge/React-20232A?style=for-the-badge&logo=react&logoColor=61DAFB
[React-url]: https://reactjs.org/
[Vue.js]: https://img.shields.io/badge/Vue.js-35495E?style=for-the-badge&logo=vuedotjs&logoColor=4FC08D
[Vue-url]: https://vuejs.org/
[Angular.io]: https://img.shields.io/badge/Angular-DD0031?style=for-the-badge&logo=angular&logoColor=white
[Angular-url]: https://angular.io/
[Svelte.dev]: https://img.shields.io/badge/Svelte-4A4A55?style=for-the-badge&logo=svelte&logoColor=FF3E00
[Svelte-url]: https://svelte.dev/
[Laravel.com]: https://img.shields.io/badge/Laravel-FF2D20?style=for-the-badge&logo=laravel&logoColor=white
[Laravel-url]: https://laravel.com
[Bootstrap.com]: https://img.shields.io/badge/Bootstrap-563D7C?style=for-the-badge&logo=bootstrap&logoColor=white
[Bootstrap-url]: https://getbootstrap.com
[JQuery.com]: https://img.shields.io/badge/jQuery-0769AD?style=for-the-badge&logo=jquery&logoColor=white
[JQuery-url]: https://jquery.com
[docker-url]: https://www.docker.com/
[dotnet-url]: https://dotnet.microsoft.com/
[gha-url]: https://github.com/features/actions
[devcontainer-url]: https://code.visualstudio.com/docs/devcontainers/containers
[csharp-url]: https://learn.microsoft.com/dotnet/csharp/
