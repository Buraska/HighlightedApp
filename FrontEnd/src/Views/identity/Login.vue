<template>
  <div class="container">

  <h1>Login</h1>
  <div class="row">
    <div class="col-md-4">
      <div v-if="errorHappened" class="text-danger validation-summary-errors" data-valmsg-summary="true">
        <ul>
          <li>{{ errors }}</li>
        </ul>
      </div>

      <div>
        <div class="form-group">
          <label class="control-label" >email</label>
          <input v-model="email" class="form-control" type="text" />
        </div>
        <div class="form-group">
          <label class="control-label" >password</label>
          <input  v-model="password" class="form-control" type="password" />
        </div>
        <div class="form-group">
          <input @click="loginClicked()" type="submit" value="Login" class="btn btn-primary" />
        </div>
      </div>
    </div>
  </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from "vue";
import { useRouter } from "vue-router";
import { AppUserService } from "@/Services/AppUserService";
import { useAppUserStore } from "@/Stores/AppUserStore";

const router = useRouter();
const appUserStore = useAppUserStore();
const service = new AppUserService();

const email = ref("");
const password = ref("");
const errorHappened = ref(false);
const errors = ref("");

async function loginClicked() {
  console.log("LoginClicked()");
  const res = await service.login(email.value, password.value);

  if (appUserStore.$state.jwt) {
    router.push("/");
  }
  console.log(res);
}
</script>
