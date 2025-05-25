import React from 'react';
import { LobbyPage } from './pages/LobbyPage';
import { GamePage } from './pages/GamePage';
import { useGameStore } from './state/gameStore';

function App() {
  const connection = useGameStore(state => state.connection);
  return (
    <div className="bg-gray-900 text-white min-h-screen p-4">
      <h1 className="text-2xl font-bold text-center mb-4">Capital Clash</h1>
      {connection ? <GamePage /> : <LobbyPage />}
    </div>
  );
}

export default App;