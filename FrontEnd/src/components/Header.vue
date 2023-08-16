<template>
  <header>
    <nav  class="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
      <div class="container-fluid">
        <router-link to="/" class="navbar-brand">HL</router-link>
        <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target=".navbar-collapse" aria-controls="navbarSupportedContent"
          aria-expanded="false"
          aria-label="Toggle navigation"
        >
          <span  class="navbar-toggler-icon"></span>
        </button>
        <div class="navbar-collapse collapse d-sm-inline-flex justify-content-between">
          <ul class="navbar-nav flex-grow-1">
            <li  class="nav-item">
              <router-link to="/" class="nav-link text-dark">Home</router-link>
            </li>
            <li  class="nav-item">
              <router-link to="/books/index" class="nav-link text-dark">Books</router-link>
            </li>
            <li  class="nav-item">
              <router-link to="/authors" class="nav-link text-dark">Authors</router-link>
            </li>
          </ul>

          <ul  class="navbar-nav">

            <template v-if="appUserStore.$state.jwt == null" >
            <li class="nav-item">
              <router-link to="/identity/register" class="nav-link text-dark">Register</router-link>
            </li>
            <li class="nav-item">
              <router-link to="/identity/login" class="nav-link text-dark">Login</router-link>
            </li>
            </template>

            <template v-else>
              <li style="cursor: pointer" class="nav-item">
                <a v-on:click="appUserService.logout()" class="nav-link text-dark">Logout</a>
              </li>
            </template>
          </ul>
        </div>
      </div>
    </nav>
  </header>

</template>

<script lang="ts">
import { useAppUserStore } from "@/Stores/AppUserStore";
import { Options, Vue } from "vue-class-component";
import { RouterLink } from "vue-router";
import { AppUserService } from "@/Services/AppUserService";

@Options({
  components: {
  }
})
export default class Header extends Vue {
  appUserStore = useAppUserStore();
  appUserService = new AppUserService();



}
</script>