<template>
  <div class="container-fluid">
    <div class="row flex-column align-items-center pt-5 position-relative">

      <div class="col-8 mb-3">
        <div class="position-relative d-flex justify-content-center">
          <img v-if="vault?.img" :src="vault?.img" :alt="vault?.name" class="coverImg img-fluid">
          <img v-else
            src="https://amdmediccentar.rs/wp-content/plugins/uix-page-builder/includes/uixpbform/images/default-cover-6.jpg"
            alt="" class="coverImg img-fluid">

          <div
            class="text-light text-shadow position-absolute align-self-end d-flex flex-column justify-content-center">
            <h2 class="d-flex justify-content-center">{{ vault?.name }}</h2>
            <h5 class="d-flex justify-content-center">{{ vault?.creator.name }}</h5>
          </div>

          <div class="position-absolute trash selectable text-danger" @click="deleteVault(vault?.id)"
            v-if="account?.id == vault?.creator.id">
            <i class="mdi mdi-delete fs-1"></i>
          </div>
        </div>

      </div>

      <div class="col-2 d-flex justify-content-center">
        <h3 class="rounded bg-purple1 p-2">{{ keeps.length }} Keeps</h3>
      </div>

      <div v-if="keeps.length" class="col-10">
        <!-- <h4 v-if="!keeps.length">Vault has no Keeps :(</h4> -->
        <div class="d-flex justify-content-center">
          <div class="px-md-5 px-0 pt-3 masonry-body bg-white2">
            <KeepCard v-for="k in keeps" :key="k.id" :keep="k" :inVault="true" class="p-0">
            </KeepCard>
          </div>
        </div>
      </div>

    </div>
  </div>
</template>


<script>
import { onMounted, watchEffect } from 'vue';
import Pop from '../utils/Pop.js';
import { vaultsService } from '../services/VaultsService.js'
import { computed } from '@vue/reactivity';
import { AppState } from '../AppState.js';
import { useRoute, useRouter } from 'vue-router';
import { keepsService } from '../services/KeepsService.js';
import { router } from '../router.js';

export default {
  setup() {

    const route = useRoute();
    const router = useRouter();

    async function getVaultById() {
      try {

        let vault = await vaultsService.getVaultById(route.params.vaultId);
        await getKeepsByVault(vault.id)
      } catch (error) {
        // Pop.error(error, "[Getting Vault By Id]")
        router.push({ name: 'Home' })
        Pop.toast('That vault is Private!!!', 'info')
        AppState.activeVault = null;
      }
    }

    async function getKeepsByVault(id) {
      try {
        await vaultsService.getKeepsByVault(id)
        console.log(AppState.vaultKeeps)
      } catch (error) {
        Pop.error(error, "[Getting Keeps By Vault]")
      }
    }

    onMounted(() => {
      getVaultById()
    })

    // watchEffect(() => {
    //   if (AppState.activeVault?.isPrivate && AppState.account?.id != AppState.activeVault?.creator.id) {
    //     router.push({ name: 'Home' })
    //     Pop.toast('That vault is Private!!!', info)
    //     AppState.activeVault = null;
    //   }
    // })

    return {
      account: computed(() => AppState.account),
      vault: computed(() => AppState.activeVault),
      keeps: computed(() => AppState.vaultKeeps),

      async deleteVault(id) {
        try {
          let choice = await Pop.confirm("Are you sure you want to delete this keep?")
          if (!choice) {
            return
          }
          await vaultsService.deleteVault(id)
          Pop.success("Vault Deleted!!!")
        } catch (error) {
          Pop.error(error, "[Deleting Vault]")
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.picture {
  border-radius: 50%;
  position: absolute;
  top: 100%;
  left: 50%;
  transform: translate(-50%, -50%);
}

.text-shadow {
  text-shadow: 2px 2px 2px black;
}

.trash {
  right: 25%;
}
</style>