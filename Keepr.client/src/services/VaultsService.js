import { AppState } from "../AppState.js";
import { Keep } from "../models/Keep.js";
import { Vault } from "../models/Vault.js";
import { api } from "./AxiosService.js";
import { useRoute, useRouter } from 'vue-router';
import { router } from "../router.js";


class VaultsService {

  async getVaultById(id) {

    // if (AppState.activeVault?.isPrivate && AppState.account?.id != AppState.activeVault?.creator.id) {
    //   router.push({ name: 'Home' })
    //   Pop.toast('That vault is Private!!!', info)
    //   AppState.activeVault = null;
    // }
    try {
      const res = await api.get(`api/vaults/${id}`)

      AppState.activeVault = new Vault(res.data)

      return AppState.activeVault
    } catch (error) {
      return null;
    }

  }

  async getKeepsByVault(id) {
    const res = await api.get(`api/vaults/${id}/keeps`)
    AppState.vaultKeeps = res.data.map(k => new Keep(k))
    console.log(res.data)
  }

  async createVaultKeep(data) {
    let kId = AppState.activeKeep.id
    const res = await api.post(`api/vaultkeeps`, data)
    AppState.activeKeep.kept++;
  }

  async removeFromVault(id) {
    const res = await api.delete(`api/vaultkeeps/${id}`)
    AppState.activeKeep = null;
    AppState.vaultKeeps = AppState.vaultKeeps.filter(k => k.vaultKeepId != id)

  }

  async deleteVault(id) {
    const res = await api.delete(`api/vaults/${id}`)
    AppState.activeVault = null;
    AppState.accountVaults = AppState.accountVaults.filter(v => v.id != id)
    AppState.profileVaults = AppState.profileVaults.filter(v => v.id != id)
    router.push({ name: 'Home' })
  }

  async createVault(data) {
    const res = await api.post(`api/vaults`, data)
    console.log(res.data)
    AppState.accountVaults = [new Vault(res.data), ...AppState.accountVaults]
  }
}

export const vaultsService = new VaultsService();