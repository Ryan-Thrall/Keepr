<template>
  <nav class="navbar navbar-expand-lg bg-white2 px-3 container-fluid shadow-lg">

    <div class="row w-100 navRow">

      <!-- First Section -->
      <div class="col-4 sec-1 d-flex align-items-center justify-content-sm-start justify-content-center nav-section">
        <!-- Home Button -->
        <router-link class="btn bg-tan2 selectable me-2 homeButton" :to="{ name: 'Home' }">
          Home
        </router-link>

        <div v-if="account.id" class="dropdown">
          <button class="btn btn-sm dropdown-toggle border-0 createButton" type="button" id="dropMenu"
            data-bs-toggle="dropdown" aria-expanded="false">
            Create
          </button>
          <div class="dropdown-menu border border-dark py-0 bg-purple1 px-3" aria-labelledby="dropMenu">

            <button class="btn p-0 rounded-0 menu-item" type="button" data-bs-toggle="modal"
              data-bs-target="#createKeeperModal">new
              keep</button>
            <div class="borderBottom"></div>
            <button class="btn p-0 rounded-0 menu-item" type="button" data-bs-toggle="modal"
              data-bs-target="#createVaultModal">new vault</button>
          </div>
        </div>

      </div>

      <!-- Logo -->
      <div class="col-4 sec-2 d-flex justify-content-center align-items-center">
        <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
          <div class="d-flex flex-column align-items-center">
            <img alt="logo" src="../assets/img/Keepr-logo.png" height="45" />
          </div>
        </router-link>
      </div>


      <div class="col-4 sec-3 d-flex justify-content-end align-items-center">
        <!-- <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarText"
        aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
        <span class="navbar-toggler-icon"></span>
      </button> -->


        <div class="" id="navbarText">
          <!-- LOGIN COMPONENT HERE -->
          <Login />
        </div>
      </div>

    </div>
  </nav>
</template>

<script>
import { computed } from '@vue/reactivity';
import { AppState } from '../AppState.js';
import Login from './Login.vue'
export default {
  setup() {
    return {
      account: computed(() => AppState.account),
      showDropdown() {
        let menu = document.getElementById("dropdown-options");
        if (menu.classList.contains("d-none")) {
          menu.classList.remove("d-none");
        } else {
          menu.classList.add("d-none");
        }
      }
    }
  },
  components: { Login }
}
</script>

<style scoped>
a:hover {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

.menu-item {
  width: 100%;
  text-align: start;
}

.borderBottom {
  display: block;
  margin: 0 auto;
  width: 100%;
  padding-top: 0;
  border-bottom: 1px solid black;
}

@media screen and (min-width: 768px) {
  nav {
    height: 64px;
  }
}

@media screen and (max-width: 576px) {
  /* nav {
    position: fixed;
    bottom: 0;
  } */

  .homeButton {
    display: none;
  }

  .sec-1 {
    order: 2;
  }

  .sec-2 {
    order: 1;
  }

  .sec-3 {
    order: 3;
  }
}
</style>
