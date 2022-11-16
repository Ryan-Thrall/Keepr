<template>
  <div class="d-flex align-items-center">
    <button class="btn bg-purple1" @click="removeFromVault">Remove From Vault</button>
  </div>
</template>


<script>
import { AppState } from '../AppState.js';
import { vaultsService } from '../services/VaultsService.js';
import Pop from '../utils/Pop.js';

export default {
  setup() {
    return {

      async removeFromVault() {
        try {
          let choice = await Pop.confirm("Are you sure you want to remove this Keep from your vault? it may be an inconvenience to find it and put it back.")
          if (!choice) {
            return
          } else
            await vaultsService.removeFromVault(AppState.activeKeep.vaultKeepId)
          document.getElementById('#keeperModalVault').hide();
        } catch (error) {

        }
        // console.log(AppState.activeKeep.vaultKeepId)
      }
    }
  }
}
</script>


<style lang="scss" scoped>

</style>