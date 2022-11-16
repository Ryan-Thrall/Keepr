<template>
  <form @submit.prevent="createKeep()">
    <div class="modal-body">
      <div class="form-floating mb-3">
        <input v-model="editable.name" required type="text" class="form-control" id="keepName" placeholder="Name">
        <label for="keepName">Name</label>
      </div>

      <div class="form-floating mb-3">
        <input v-model="editable.img" required type="text" class="form-control" id="keepImg" placeholder="Image">
        <label for="keepImg">Image</label>
      </div>

      <div class="form-floating mb-3">
        <textarea v-model="editable.description" required type="text" class="form-control" id="keepDescription"
          placeholder="description">
            </textarea>
        <label for="keepDescription">Description</label>
      </div>

    </div>
    <div class="modal-footer">
      <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
      <button type="submit" class="btn btn-primary">Save changes</button>
    </div>
  </form>
</template>


<script>
import { ref } from 'vue';
import { keepsService } from '../services/KeepsService.js';

export default {
  setup() {
    const editable = ref({})
    return {
      editable,
      async createKeep() {
        try {
          await keepsService.createKeep(editable.value)
        } catch (error) {

        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>

</style>