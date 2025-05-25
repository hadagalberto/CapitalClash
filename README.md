# 🎲 Capital Clash

Capital Clash é um jogo multiplayer online inspirado em Monopoly e Business Tour, onde os jogadores se enfrentam em um tabuleiro com propriedades baseadas em capitais brasileiras. Com suporte a partidas locais sem login, é ideal para jogar com amigos de forma rápida e divertida.

---

## 🧠 Funcionalidades

- ✅ Criação e entrada em salas com nickname
- ✅ Backend em ASP.NET Core com SignalR
- ✅ Frontend em React + Vite + Tailwind CSS
- ✅ Sistema de turnos com dados reais (dupla jogada se tirar números iguais)
- ✅ Propriedades compráveis e melhoráveis
- ✅ Casas especiais como "Ir para prisão", "Sorte ou Revés", "Taxa"
- ✅ Ações: rolar dados, comprar, melhorar, pagar fiança
- ✅ Gerenciamento de estado com Zustand
- ✅ Comunicação em tempo real com SignalR

---

## 🚀 Como rodar o projeto localmente

### 🔧 Requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- [Node.js 18+](https://nodejs.org)
- Gerenciador de pacotes: `npm` ou `yarn`

### 🖥️ Backend

```bash
cd CapitalClash
dotnet restore
dotnet run
```

- Por padrão, ele roda em http://localhost:5035.

### 🌐 Frontend
```bash
cd CapitalClashFrontend
npm install
npm run dev
```

- Acesse http://localhost:5173 no navegador.
