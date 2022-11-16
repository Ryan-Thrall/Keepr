<template>
  <div class="container-fluid">
    <div class="row flex-column align-items-center pt-5">

      <div class="col-8 mb-5">
        <div class="position-relative">
          <img v-if="account?.coverImg" :src="account?.coverImg" :alt="account?.name" class="coverImg img-fluid">
          <img v-else
            src="https://amdmediccentar.rs/wp-content/plugins/uix-page-builder/includes/uixpbform/images/default-cover-6.jpg"
            alt="" class="coverImg img-fluid">
          <img :src="account?.picture" alt="" class="picture">
        </div>
      </div>

      <div class="col-4 d-flex flex-column align-items-center mt-3">
        <h2>{{ account?.name }}</h2>
        <p>{{ vaults.length }} Vaults | {{ keeps.length }} Keeps</p>
        <p></p>
      </div>

      <div class="col-10 mb-4 d-flex flex-column">
        <h3 class="mb-2">Vaults</h3>
        <h4 v-if="!vaults.length">User has no Vaults :(</h4>
        <div v-else class="">
          <VaultCard v-for="v in vaults" :key="v.id" :vault="v" />
        </div>
      </div>

      <div class="col-10">
        <h3 class="mb-2">Keeps</h3>
        <h4 v-if="!keeps.length">User has no Keeps :(</h4>
        <div v-else class="d-flex justify-content-center">
          <div class="px-md-5 px-0 pt-3 masonry-body bg-white2">
            <KeepCard v-for="k in keeps" :key="k.id" :keep="k" class="p-0" />
          </div>
        </div>
      </div>

    </div>
  </div>
</template>

<script>
import { computed, onMounted } from 'vue'
import { AppState } from '../AppState'
import { accountService } from '../services/AccountService.js';
import Pop from '../utils/Pop.js';
export default {
  setup() {

    // async function getKeepsByAccount() {
    //   try {
    //     if (!AppState.account?.id) {
    //       return
    //     }
    //     await accountService.getKeepsByAccount();
    //   } catch (error) {
    //     Pop.error(error, "[Getting Keeps By Account]")
    //   }
    // }
    // async function getVaultsByAccount() {
    //   try {
    //     await accountService.getVaultsByAccount();
    //   } catch (error) {
    //     Pop.error(error, "[Getting Vaults By Account]")
    //   }
    // }

    // onMounted(() => {
    //   getKeepsByAccount();
    //   getVaultsByAccount();
    // });

    return {
      account: computed(() => AppState.account),
      keeps: computed(() => AppState.accountKeeps),
      vaults: computed(() => AppState.accountVaults),
    }
  }
}
</script>

<style scoped>
.picture {
  border-radius: 50%;
  position: absolute;
  top: 100%;
  left: 50%;
  transform: translate(-50%, -50%);
}

.masonry-body {
  width: 100%;
  column-count: 4;
  column-gap: 1rem;
  /* column-width: 8rem; */
}



@media screen and (max-width: 576px) {
  .masonry-body {
    column-count: 2;
    /* column-gap: 0.2rem; */
  }
}
</style>
