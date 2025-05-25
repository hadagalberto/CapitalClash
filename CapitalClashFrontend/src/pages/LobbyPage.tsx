import React, { useState } from 'react';
import { useGameStore } from '../state/gameStore';
import { HubConnectionBuilder } from '@microsoft/signalr';

export const LobbyPage = () => {
  const [nickname, setNickname] = useState('');
  const [roomCode, setRoomCode] = useState('');
  const [error, setError] = useState('');
  const setConnection = useGameStore(state => state.setConnection);
  const setPlayerInfo = useGameStore(state => state.setPlayerInfo);

  const connect = async (roomCode: string, nickname: string) => {
  const connection = new HubConnectionBuilder()
    .withUrl('http://localhost:5035/gamehub')
    .withAutomaticReconnect()
    .build();

  // ⚠️ Os eventos devem ser registrados ANTES do start()
  connection.on('PlayerJoined', (name) =>
    console.log(`🟢 ${name} entrou na sala.`)
  );

  connection.on('LoadBoard', (spaces) => {
    console.log('📦 Tabuleiro recebido no Lobby:', spaces);
    useGameStore.getState().setSpaces(spaces);
  });

  connection.on('UpdatePlayers', (players) => {
    console.log('👥 Jogadores atualizados no Lobby:', players);
    useGameStore.getState().updatePlayers(players);
  });

  await connection.start();

  const response = await connection.invoke('Ping');
  console.log("Ping:", response);

  console.log("Conectado ao SignalR");
  console.log("Conectando à sala:", roomCode);
  console.log("Nickname:", nickname);

  await connection.invoke('JoinRoom', roomCode, nickname, null);

  setConnection(connection);
  setPlayerInfo(nickname, roomCode);
};


  const createRoom = async () => {
    const res = await fetch('http://localhost:5035/rooms', { method: 'POST' });
    const code = JSON.parse(await res.text()); // retorna "79C7D6"
    await connect(code, nickname);
  };

  const joinRoom = async () => {
    try {
      await connect(roomCode, nickname);
    } catch {
      setError('Falha ao entrar na sala. Verifique o código e tente novamente.');
    }
  };

  return (
    <div className="text-white space-y-4 max-w-sm mx-auto mt-10">
      <h2 className="text-xl font-bold">Entrar no Jogo</h2>
      <input placeholder="Seu apelido" value={nickname} onChange={e => setNickname(e.target.value)} className="w-full text-black p-2" />
      <input placeholder="Código da sala" value={roomCode} onChange={e => setRoomCode(e.target.value)} className="w-full text-black p-2" />
      <button onClick={joinRoom} className="bg-blue-600 p-2 w-full">Entrar</button>
      <button onClick={createRoom} className="bg-green-600 p-2 w-full">Criar Sala</button>
      {error && <p className="text-red-400">{error}</p>}
    </div>
  );
};