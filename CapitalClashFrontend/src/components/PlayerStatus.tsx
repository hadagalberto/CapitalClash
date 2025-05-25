
import React from 'react';
import { useGameStore } from '../state/gameStore';

export const PlayerStatus = () => {
  const players = useGameStore(state => state.players);

  return (
    <div className="bg-gray-800 p-2 text-sm rounded">
      <h2 className="text-lg font-bold mb-2">Jogadores</h2>
      <ul>
        {players.map((p) => (
          <li key={p.id}>
            <span style={{ color: p.color }}>{p.nickname}</span> - posição: {p.position} - saldo: R${p.balance ?? '?'}
          </li>
        ))}
      </ul>
    </div>
  );
};
