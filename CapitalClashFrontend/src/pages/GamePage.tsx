import React, { useEffect } from 'react';
import { useGameStore } from '../state/gameStore';
import { Board } from '../components/Board';
import { GameLog } from '../components/GameLog';
import { PlayerStatus } from '../components/PlayerStatus';

export const GamePage = () => {
  const connection = useGameStore(state => state.connection);
  const roomCode = useGameStore(state => state.roomCode);
  const setSpaces = useGameStore(state => state.setSpaces);
  const updatePlayers = useGameStore(state => state.updatePlayers);

  const sendAction = async (method: string) => {
    if (connection) {
      await connection.invoke(method, roomCode);
    }
  };

  useEffect(() => {
    if (!connection) {
      console.error('Conexão não estabelecida.');
      return;
    }

    connection.on('LoadBoard', (spaces) => {
      console.log('📦 Tabuleiro carregado:', spaces);
      setSpaces(spaces);
    });

    connection.on('UpdatePlayers', (players) => {
      updatePlayers(players);
    });
  }, [connection, setSpaces, updatePlayers]);

  return (
    <div className="space-y-4">
      <Board />
      <div className="flex flex-wrap gap-2 justify-center mt-4">
        <button onClick={() => sendAction('RollDice')} className="bg-blue-600 px-4 py-2 rounded">🎲 Rolar Dados</button>
        <button onClick={() => sendAction('BuyProperty')} className="bg-green-600 px-4 py-2 rounded">🏠 Comprar</button>
        <button onClick={() => sendAction('UpgradeProperty')} className="bg-yellow-600 px-4 py-2 rounded">⬆️ Melhorar</button>
        <button onClick={() => sendAction('PayJailFine')} className="bg-red-600 px-4 py-2 rounded">💸 Pagar Fiança</button>
      </div>
      <PlayerStatus />
      <GameLog />
    </div>
  );
};
