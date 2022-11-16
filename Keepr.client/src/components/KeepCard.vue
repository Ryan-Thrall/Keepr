<template>
  <div :style="{ backgroundImage: `url(${keep.img})`, height: `${randomHeight(15, 35)}rem` }"
    class="keepCard rounded shadow selectable" data-bs-toggle="modal"
    :data-bs-target="inVault ? '#keeperModalVault' : '#keeperModal'" @click="getKeepById(keep.id, inVault)">

    <div class="h-100 d-flex align-items-end p-2 justify-content-between">
      <h4 class="keeperName text-light text-shadow">{{ keep.name }}</h4>
      <!-- {{ k.creator }}  -->
      <img :src="keep.creator.picture" alt="" class="keeprProfile selectable" :title="keep.creator.name">
    </div>
  </div>

</template>


<script>
import { AppState } from '../AppState.js';
import { Keep } from '../models/Keep.js';
import { keepsService } from '../services/KeepsService.js';
import Pop from '../utils/Pop.js';
// import ModalComponent from './KeepModal.vue';

export default {
  props: {
    keep: {
      type: Keep,
      required: true
    },
    inVault: {
      type: Boolean,
    },
  },
  setup(props) {
    return {

      async getKeepById(keepId, inVault) {
        try {
          let keep = await keepsService.getKeepById(keepId)
          if (inVault) {
            let vk = AppState.vaultKeeps.find(k => k.id == keep.id)
            AppState.activeKeep.vaultKeepId = vk.vaultKeepId;
          }
        } catch (error) {
          Pop.error(error, "[Getting Keep By Id]")
        }

      },

      async deleteKeepFromVault() {

      },

      randomHeight(min, max) {
        min = Math.ceil(min);
        max = Math.floor(max);
        return Math.floor(Math.random() * (max - min + 1) + min);
      }
    }
  },
  // components: { ModalComponent }
}
</script>


<style lang="scss" scoped>
.keepCard {
  background-size: cover;
  background-position: center;
  margin: 0.5rem;
  width: 100%;
  // height: 10rem;
  display: inline-block;
  // background: linear-gradient(to top, rgba(0, 0, 0, 0.85), transparent);
}

.text-shadow {
  text-shadow: 2px 2px 2px black;
}

.keeprProfile {
  border-radius: 50%;
  width: 3rem;
  height: 3rem;
}

.delete {
  top: 0.25rem;
  right: 0.25rem;
}

// .keeprProfile:hover {
//   width: 3.2rem;
//   height: 3.2rem;
// }
</style>