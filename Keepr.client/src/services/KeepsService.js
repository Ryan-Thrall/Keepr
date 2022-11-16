import { Keep } from "../models/Keep.js";
import { api } from "./AxiosService.js";
import { AppState } from '../AppState.js'
import { Profile } from "../models/Account.js";


class KeepsService {

  async GetKeeps() {
    const res = await api.get('api/keeps')
    // console.log(res.data)
    AppState.keeps = res.data.map(k => new Keep(k))
  }

  async getKeepById(keepId) {
    const res = await api.get(`api/keeps/${keepId}`)
    AppState.activeKeep = new Keep(res.data)
    console.log(AppState.activeKeep)
    return AppState.activeKeep
  }

  async deleteKeep(keepId) {
    const res = await api.delete(`api/keeps/${keepId}`)
    AppState.activeKeep = null;
    AppState.accountKeeps = AppState.accountKeeps.filter(k => k.id != keepId)
    AppState.keeps = AppState.keeps.filter(k => k.id != keepId)
  }

  async createKeep(data) {
    const res = await api.post(`api/keeps`, data)
    console.log(res.data)
    AppState.accountKeeps = [new Keep(res.data), ...AppState.accountKeeps]
    AppState.keeps = [new Keep(res.data), ...AppState.keeps]
  }

}

export const keepsService = new KeepsService();