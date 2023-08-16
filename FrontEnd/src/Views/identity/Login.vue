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



<script lang="ts">
import { Vue } from "vue-class-component";
import { AppUserService } from "@/Services/AppUserService";
import {useAppUserStore} from "@/Stores/AppUserStore";

export default class Login extends Vue {
  appUserStore = useAppUserStore();

  email = "";
  password = "";
  service = new AppUserService();
  errorHappened = false;


  async loginClicked(){
    console.log("LoginClicked()");
    var res = await this.service.login(this.email, this.password);

    if (this.appUserStore.$state.jwt){
      this.$router.push('/')
    }
    console.log(res);
  }
};
</script>
