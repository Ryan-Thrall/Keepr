import { AppState } from '../AppState'
import { Profile } from '../models/Account.js'
import { Keep } from '../models/Keep.js'
import { Vault } from '../models/Vault.js'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = res.data
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }

  async getProfileById(id) {
    const res = await api.get(`/api/profiles/${id}`)
    AppState.activeProfile = new Profile(res.data)
    console.log(AppState.activeProfile)
    return AppState.activeProfile
  }

  async getKeepsByProfile(id) {
    const res = await api.get(`api/profiles/${id}/keeps`)
    AppState.profileKeeps = res.data.map(k => new Keep(k))
  }

  async getVaultsByProfile(id) {
    const res = await api.get(`api/profiles/${id}/vaults`)
    AppState.profileVaults = res.data.map(k => new Vault(k))
  }

  async getKeepsByAccount() {
    let id = AppState.account.id
    const res = await api.get(`api/profiles/${id}/keeps`)
    AppState.accountKeeps = res.data.map(k => new Keep(k))
  }

  async getVaultsByAccount() {

    const res = await api.get(`/account/vaults`)
    AppState.accountVaults = res.data.map(k => new Vault(k))
  }
}
export const accountService = new AccountService()
