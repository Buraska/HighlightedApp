<template>
  <main role="main" class="pb-1">

    <h1>Register</h1>

    <h2>Create a new account.</h2>
    <hr>
    <div v-if="errors.length !== 0" class="text-danger validation-summary-errors" data-valmsg-summary="true">
      <div v-for="error in errors">
        <ErrorMessage :message="error"></ErrorMessage>
      </div>
    </div>

    <div class="row">
      <div class="col-md-4">
          <div class="form-floating m-1">
            <input class="form-control" type="email" data-val="true"  id="Input_Email" name="Input.Email" v-model="email">
            <label for="Input_Email">Email</label>
          </div>

          <div class="form-floating m-1">
            <input class="form-control" type="password" id="Input_Password" v-model="password" >
            <label for="Input_Password">Password</label>
          </div>
          <div class="form-floating m-1">
            <input class="form-control" type="password" id="Input_ConfirmPassword" name="Input.ConfirmPassword" v-model="confirmPassword">
            <label for="Input_ConfirmPassword">Confirm password</label>
            <span class="text-danger field-validation-valid" data-valmsg-for="Input.ConfirmPassword" data-valmsg-replace="true"></span>
          </div>
          <button v-on:click="registerClicked" class="w-100 btn btn-lg btn-primary">Register</button>
      </div>
      <div class="col-4">
        <div class="form-floating m-1">
          <input class="form-control" type="text" data-val="true"  id="Input_fName" v-model="fName">
          <label for="Input_fName">First Name</label>
        </div>
        <div class="form-floating m-1">
          <input class="form-control" type="text" data-val="true"  id="Input_lName" v-model="lName">
          <label for="Input_fName">First Name</label>
        </div>
      </div>
    </div>
  </main>

</template>

<script lang="ts">
import { Options, Vue } from "vue-class-component";
import { AppUserService } from "@/Services/AppUserService";
import ErrorMessage from "@/components/ErrorMessage.vue";
import { AxiosError } from "axios";

@Options({components: {ErrorMessage}})

export default class Register extends Vue{
  appUserService = new AppUserService();

  errors: string[] = [];
  email = "";
  password = "";
  confirmPassword = "";
  fName = "";
  lName = "";

  async registerClicked(){
    this.errors = [];

    if (this.email.length === 0 || this.email.length > 128){
      this.errors.push("Email length should be in range of 1 and 128 characters")
    }

    if (this.password.length === 0 || this.password.length > 128){
      this.errors.push("Password length should be in range of 1 and 128 characters")
    }

    if (this.fName.length === 0 || this.lName.length > 128){
      this.errors.push("First name length should be in range of 1 and 128 characters")
    }

    if (this.lName.length === 0 || this.lName.length > 128){
      this.errors.push("Last name length should be in range of 1 and 128 characters")
    }

    if (this.password !== this.confirmPassword){
      this.errors.push("Passwords do not match to each other")
    }


    var validRegex = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;

    if (!this.email.match(validRegex)){
      this.errors.push("Not proper email")
    }

    if (this.errors.length !== 0){
      return;
    }

    var res = await this.appUserService.register(this.email, this.password, this.fName, this.lName);

    if (res.status === 400){
      this.errors.push("Email is already registered");
    }

    if (this.errors.length !== 0){
      return;
    }

    this.$router.push('/')

  }

};
</script>

<style scoped>

</style>