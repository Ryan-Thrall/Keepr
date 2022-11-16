<template>
  <div class="d-flex align-items-center justify-content-center" v-if="account.id">
    <!-- {{ vaults }} -->
    <form @submit.prevent="saveToVault">
      <select name="vault" class="ms-4 me-3" id="vault" v-model="editable.vaultId">
        <!-- <option selected>Pick a Vault</option> -->
        <option v-for="v in vaults" :value="v.id">{{ v.name }}</option>
        <!-- <option value="">Placeholder1</option>
      <option value="">Small 2</option>
      <option value="">aaaaaaaaaa</option> -->
      </select>
      <button class="btn bg-purple1" type="submit">Save</button>
    </form>
  </div>
</template>


<script>
import { computed } from '@vue/reactivity';
import { onMounted } from 'vue';
import { ref } from 'vue';
import { AppState } from '../AppState.js';
import { vaultsService } from '../services/VaultsService.js';
import Pop from '../utils/Pop.js';

export default {
  setup() {
    const editable = ref({})
    return {
      editable,
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.accountVaults),

      async saveToVault() {
        try {
          console.log(editable.value.vaultId)
          if (!editable.value.vaultId) {
            Pop.error("Please Select a Vault")
            return
          }
          editable.value.keepId = AppState.activeKeep.id
          await vaultsService.createVaultKeep(editable.value)
          Pop.success("Keep Saved Successfully")
        } catch (error) {
          Pop.error(error, "[Saving to Vault]")
        }
      }


    }
  }
}
</script>


<style lang="scss" scoped>

</style>