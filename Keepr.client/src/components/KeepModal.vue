<template>
  <div class="modal modal-xl fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel"
    aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered">
      <div class="modal-content">
        <div class="modal-body p-0 position-relative">
          <div class="position-absolute trash selectable" @click="deleteKeep()" v-if="account?.id == keep?.creator.id">
            <i class="mdi mdi-delete fs-1"></i>
          </div>
          <div class="containter-fluid" v-if="keep != null">
            <div class="row bg-white2 rounded mobile">
              <div class="col-md-6 col-12 p-0">
                <img :src="keep?.img" alt="" class="image rounded-start">
              </div>
              <div class="col-md-6 col-12">
                <div class="row flex-column py-3 h-100 justify-content-between">
                  <!-- First Row -->
                  <div class="col-12 rounded d-flex justify-content-center align-items-center py-1">
                    <i class="mdi mdi-eye-outline fs-4 me-1" title="Views"></i>
                    <p class="m-0 me-3">{{ keep?.views }}</p>
                    <i class="mdi mdi-alpha-k-box-outline fs-4 me-1" title="Kept"></i>
                    <p class="m-0">{{ keep?.kept }}</p>
                  </div>

                  <div class="col-12 mb-5">
                    <div class="d-flex flex-column align-items-center justify-content-between">
                      <h1 class="mb-5">{{ keep?.name }}</h1>
                      <p>{{ keep?.description }}</p>
                    </div>
                  </div>



                  <div class="col-12 d-flex flex-md-row flex-column pb-2 pb-md-0 justify-content-between">
                    <slot>

                    </slot>

                    <router-link
                      :to="account?.id == keep?.creator.id ? { name: 'Account' } : { name: 'Profile', params: { profileId: keep?.creator.id } }">
                      <div class="d-flex align-items-center selectable p-1 justify-content-center"
                        data-bs-dismiss="modal">
                        <img :src="keep?.creator.picture" alt="" class="keeprProfile me-2">
                        <p class="m-0 text-dark"><strong>{{ keep?.creator.name }}</strong></p>
                      </div>
                    </router-link>

                  </div>

                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { computed } from '@vue/reactivity';
import { Modal } from 'bootstrap';
import { AppState } from '../AppState.js';
import { keepsService } from '../services/KeepsService.js';
import Pop from '../utils/Pop.js';

export default {
  setup() {
    // let m = Modal.getOrCreateInstance('#keep');
    // m.hide()
    return {
      account: computed(() => AppState.account),
      keep: computed(() => AppState.activeKeep),

      async deleteKeep() {
        try {
          let choice = await Pop.confirm("Are you sure you want to delete this keep?")
          if (!choice) {
            return
          }
          await keepsService.deleteKeep(AppState.activeKeep.id)
          Pop.success("Keep Deleted!!!")
        } catch (error) {
          Pop.error(error, "[[Deleting Keep]]")
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.image {
  object-fit: cover;
  // object-position:;
  width: 100%;
  height: 70vh;
}

.small-col {
  max-height: 4rem;
}

.keeprProfile {
  border-radius: 50%;
  width: 3rem;
  height: 3rem;
}

.trash {
  right: 0;
}

// .keeprProfile:hover {
//   width: 3.2rem;
//   height: 3.2rem;
// }
// @media screen and (max-width: 576px) {}
</style>