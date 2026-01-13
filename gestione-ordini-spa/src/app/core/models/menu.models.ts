import { TaskJson } from './task.models';

export interface MenuItem {
  id: number;
  nome: string;
  prezzo: number;
  tipo: number;
  prodottiInOrdine: unknown;
}

export interface MenuResponse {
  menu: {
    menu: TaskJson<MenuItem[]>;
  };
}
