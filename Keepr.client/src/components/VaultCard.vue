<template>
  <router-link :to="{ name: 'Vault', params: { vaultId: vault.id } }">
    <div :style="{ backgroundImage: `url(${vault.img})` }"
      class="vaultCard rounded shadow selectable mx-2 my-1 position-relative">

      <div class="h-100 d-flex align-items-end p-2 justify-content-between">
        <h4 class="keeperName text-light text-shadow">{{ vault.name }}</h4>
      </div>

      <div v-if="vault.isPrivate" class="position-absolute lock">
        <i class="mdi mdi-lock"></i>
      </div>
    </div>
  </router-link>
</template>


<script>
import { computed } from '@vue/reactivity';
import { AppState } from '../AppState.js';
import { Vault } from '../models/Vault.js';

export default {
  props: {
    vault: {
      type: Vault,
      required: true
    }
  },
  setup() {
    return {
      account: computed(() => AppState.account)
    }
  }
}
</script>


<style lang="scss" scoped>
.vaultCard {
  width: 12rem;
  height: 10rem;
  display: inline-block;
}

.text-shadow {
  text-shadow: 2px 2px 2px black;
}

.lock {
  bottom: 0;
  right: 0.25rem;
}

.trash {
  right: 0;
}
</style>