# Pokémon Battle Scene Challenge

Este projeto foi desenvolvido como parte de um desafio técnico para uma vaga na área de desenvolvimento de jogos.  
A proposta consistia em construir uma cena de batalha inspirada nos jogos clássicos da franquia Pokémon, utilizando dados em tempo real da PokéAPI.

---

## 🎬 Animação de entrada

A abertura da cena simula uma Pokébola se abrindo, utilizando dois overlays hemisféricos retraindo radialmente de cima para baixo.

![Pokémon Battle Scene](pokemon_battle_scene.gif)

---

## 🎮 Funcionalidades implementadas

- ✅ Nome, sprite, nível e HP dos Pokémon aliado e inimigo
- ✅ Até 4 golpes carregados via PokéAPI, com fallback para "--" nos slots vazios
- ✅ Exibição dinâmica do tipo e PP do golpe selecionado
- ✅ Navegação entre golpes utilizando mouse e teclado (setas)
- ✅ Mensagem "What will [POKÉMON] do?" exibida com quebra de linha
- ✅ HP calculado com base na fórmula oficial da franquia (sem IV e EV)

---

## 🌟 Extras implementados

- 🎨 Animação de fade-in com dois overlays hemisféricos (superior e inferior), retraindo radialmente de 0.5 para 0 — simulando uma Pokébola se abrindo
- 🔁 Fade-out reverso ao pressionar barra de espaço, recarregando a cena com novos Pokémon aleatórios
- 🧪 Compatível com Pokémon que possuem menos de 4 movimentos (ex: Ditto)
- 🧩 Estrutura modular preparada para futuras extensões, como sumário e barra de HP dinâmica
- 🌄 Randomização de tiles/background de batalha

---

## 📦 Tecnologias utilizadas

- Unity 6 (6000.1.0f1)
- C#
- TextMeshPro
- PokéAPI (https://pokeapi.co/)
- Coroutines, UnityWebRequest, UI dinâmica

---

## 🔄 Como testar

1. Clonar o repositório
2. Abrir o projeto no Unity
3. Executar a cena `BattleScene`
4. Pressionar **barra de espaço** para simular fade-out e carregar novos Pokémon aleatórios
5. Navegue através de setas do teclado e/ou clique do mouse

---

## 🧠 Considerações

- As opções “Bag” e “Run” foram desativadas, com espaço reservado para mensagens de erro
- A implementação de combate, sons ou animações de ataque **não foi solicitada** e foi intencionalmente omitida
- Alguns recursos adicionais estão planejados como extras, como:
  - HP atual randômico com atualização visual na barra
  - Tela de sumário com EXP e detalhes adicionais

---

## 👤 Autor

Desenvolvido por **Mateus Assis**  
Contato: [mateusoassis@gmail.com]
