import { HubConnection } from '@microsoft/signalr';
import { create } from 'zustand';

export interface Player {
  id: string;
  nickname: string;
  position: number;
  balance: number;
  color: string;
}

export interface BoardSpace {
  index: number;
  name: string;
  type: string;
  cost?: number;
  rent?: number;
  ownerId?: string | null;
}

interface GameState {
  nickname: string;
  roomCode: string;
  connection: HubConnection | null;
  spaces: BoardSpace[];
  players: Player[];
  setPlayerInfo: (nickname: string, roomCode: string) => void;
  setConnection: (conn: HubConnection) => void;
  updatePlayers: (players: Player[]) => void;
  setSpaces: (spaces: BoardSpace[]) => void;
}

export const useGameStore = create<GameState>((set) => ({
  nickname: '',
  roomCode: '',
  connection: null,
  spaces: [],
  players: [],

  setPlayerInfo: (nickname: string, roomCode: string) =>
    set({ nickname, roomCode }),

  setConnection: (connection: any) =>
    set({ connection }),

  updatePlayers: (players: any[]) =>
    set({ players }),

  setSpaces: (spaces: any[]) =>
    set({ spaces }),
}));