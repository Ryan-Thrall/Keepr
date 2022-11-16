import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  /** @type {import('./models/Account.js').Account} */
  account: {},

  keeps: [],

  activeKeep: null,

  activeVault: null,

  vaultKeeps: [],

  activeProfile: null,

  profileVaults: [],

  profileKeeps: [],

  accountVaults: [],

  accountKeeps: [],


})
