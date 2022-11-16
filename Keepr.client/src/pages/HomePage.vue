<template>
  <div class="d-flex justify-content-center">
    <div class="px-md-5 px-0 pt-3 masonry-body bg-white2">
      <KeepCard v-for="k in keeps" :key="k.id" :keep="k" class="p-0" />
    </div>
  </div>
</template>

<script>
import { computed } from '@vue/reactivity';
import { onMounted } from 'vue';
import { AppState } from '../AppState.js'
import { keepsService } from '../services/KeepsService.js'
import KeepCard from '../components/KeepCard.vue'
import Pop from '../utils/Pop.js';

export default {
  setup() {
    async function GetKeeps() {
      try {
        await keepsService.GetKeeps();
      } catch (error) {
        Pop.error(error, "[Getting Keeps]")
      }
    }

    onMounted(() => {
      GetKeeps();
    })

    return {
      keeps: computed(() => AppState.keeps),
    }
  }
}
</script>

<style scoped lang="scss">
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
