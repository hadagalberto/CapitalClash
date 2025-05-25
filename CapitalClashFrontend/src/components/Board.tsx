import React from 'react';
import { useGameStore } from '../state/gameStore';

export const Board = () => {
  const spaces = useGameStore(state => state.spaces);
  const players = useGameStore(state => state.players);

  const getPlayersInSpace = (index: number) =>
    players.filter(p => p.position === index);

  if (!spaces.length) {
    return <p className="text-white text-center">⏳ Carregando tabuleiro...</p>;
  }

  return (
    <div className="grid grid-cols-5 grid-rows-5 gap-1 w-[500px] h-[500px]">
      {spaces.map(space => (
        <div key={space.index} className="border p-1 bg-slate-800 text-center text-sm relative">
          <div className="font-bold">{space.name}</div>
          <div className="absolute bottom-1 left-1 right-1 flex flex-wrap justify-center gap-1">
            {getPlayersInSpace(space.index).map(player => (
              <span
                key={player.id}
                className="text-white text-xs px-1 rounded"
                style={{ backgroundColor: player.color }}
              >
                {player.nickname}
              </span>
            ))}
          </div>
        </div>
      ))}
    </div>
  );
};
