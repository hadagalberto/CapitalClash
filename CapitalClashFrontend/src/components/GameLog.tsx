
import React, { useEffect, useState } from 'react';
import { useGameStore } from '../state/gameStore';

export const GameLog = () => {
  const connection = useGameStore(state => state.connection);
  const [log, setLog] = useState<string[]>([]);

  const addLog = (msg: string) => {
    setLog((prev) => [...prev.slice(-99), msg]);
  };

  useEffect(() => {
    if (!connection) return;

    connection.on('DiceRolled', (player, d1, d2) =>
      addLog(`🎲 ${player} rolou ${d1} + ${d2}`)
    );
    connection.on('PlayerJoined', (player) =>
      addLog(`🟢 ${player} entrou na sala`)
    );
    connection.on('PlayerLeft', (player) =>
      addLog(`🔴 ${player} saiu do jogo`)
    );
    connection.on('NextTurn', (player) =>
      addLog(`🔁 Turno de ${player}`)
    );
    connection.on('SentToJail', (player) =>
      addLog(`🚔 ${player} foi preso`)
    );
    connection.on('ExtraTurn', (player) =>
      addLog(`🔄 ${player} ganhou uma rodada extra`)
    );
    connection.on('ChanceResult', (player, msg) =>
      addLog(`🎴 ${player}: ${msg}`)
    );
    connection.on('Bankrupt', (player) =>
      addLog(`☠️ ${player} está falido`)
    );
    connection.on('RentPaid', (payer, owner, prop, amount) =>
      addLog(`💸 ${payer} pagou R$${amount} para ${owner} em ${prop}`)
    );
    connection.on('BonusStart', (player, amount) =>
      addLog(`🏁 ${player} ganhou R$${amount} ao passar pelo Início`)
    );
  }, [connection]);

  return (
    <div className="bg-gray-800 p-2 max-h-64 overflow-y-auto text-sm rounded">
      {log.map((msg, idx) => <div key={idx}>{msg}</div>)}
    </div>
  );
};
