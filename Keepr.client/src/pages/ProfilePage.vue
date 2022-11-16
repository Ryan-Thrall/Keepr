<template>
  <div class="container-fluid">
    <div class="row flex-column align-items-center pt-5">

      <div class="col-8 mb-5">
        <div class="position-relative">
          <img v-if="profile?.coverImg" :src="profile?.coverImg" :alt="profile?.name">
          <img v-else
            src="https://amdmediccentar.rs/wp-content/plugins/uix-page-builder/includes/uixpbform/images/default-cover-6.jpg"
            alt="" class="coverImg img-fluid">
          <img :src="profile?.picture" alt="" class="picture">
        </div>
      </div>

      <div class="col-4 d-flex flex-column align-items-center mt-3">
        <h2>{{ profile?.name }}</h2>
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
import { computed } from '@vue/reactivity';
// import { profile } from 'console';
import { onMounted, watchEffect } from 'vue';
import { useRoute } from 'vue-router';
import { AppState } from '../AppState.js';
import { accountService } from '../services/AccountService.js';
import { keepsService } from '../services/KeepsService.js';
import Pop from '../utils/Pop.js';

export default {
  setup() {

    const route = useRoute();

    async function getProfileById() {
      try {
        let profile = await accountService.getProfileById(route.params.profileId);
        await getKeepsByProfile(profile.id);
        await getVaultsByProfile(profile.id);
      } catch (error) {
        Pop.error(error, "[Getting Profile]")
      }
    }

    async function getKeepsByProfile(id) {
      try {
        await accountService.getKeepsByProfile(id);
      } catch (error) {
        Pop.error(error, "[Getting Keeps By Profile]")
      }
    }
    async function getVaultsByProfile(id) {
      try {
        await accountService.getVaultsByProfile(id);
      } catch (error) {
        Pop.error(error, "[Getting Vaults By Profile]")
      }
    }

    onMounted(() => {
      getProfileById();
    });



    return {
      account: computed(() => AppState.account),
      profile: computed(() => AppState.activeProfile),
      keeps: computed(() => AppState.profileKeeps),
      vaults: computed(() => AppState.profileVaults)
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

.masonry-body {
  width: 100%;
  column-count: 4;
  column-gap: 1rem;
  // column-width: 8rem;
}



@media screen and (max-width: 576px) {
  .masonry-body {
    column-count: 2;
    // column-gap: 0.2rem;
  }
}
</style>