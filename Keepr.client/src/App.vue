<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />
  </main>
  <footer>
  </footer>

  <ModalComponent id="keeperModal">
    <SaveToVault />
  </ModalComponent>
  <ModalComponent id="keeperModalVault">
    <DeleteFromVault />
  </ModalComponent>

  <CreateModal id="createKeeperModal">
    <KeeperForm />
  </CreateModal>
  <CreateModal id="createVaultModal">
    <VaultForm></VaultForm>
  </CreateModal>
  <!-- </Modal> -->
</template>

<script>
import { computed, onMounted } from 'vue'
import { AppState } from './AppState'
import CreateModal from './components/CreateModal.vue'
import DeleteFromVault from './components/DeleteFromVault.vue'
import KeeperForm from './components/KeeperForm.vue'
import VaultForm from './components/VaultForm.vue'
import ModalComponent from './components/KeepModal.vue'
import Navbar from './components/Navbar.vue'
import SaveToVault from './components/SaveToVault.vue'
import Pop from './utils/Pop.js'

export default {
  setup() {
    return {
      appState: computed(() => AppState)
    }
  },
  components: { Navbar, ModalComponent, SaveToVault, DeleteFromVault, CreateModal, KeeperForm }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root {
  --main-height: calc(100vh - 32px - 64px);
}


footer {
  display: grid;
  place-content: center;
  height: 32px;
}
</style>
