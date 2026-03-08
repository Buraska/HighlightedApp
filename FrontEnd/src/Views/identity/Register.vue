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

<script setup lang="ts">
import { ref } from "vue";
import { useRouter } from "vue-router";
import { AppUserService } from "@/Services/AppUserService";
import ErrorMessage from "@/components/ErrorMessage.vue";

const router = useRouter();
const appUserService = new AppUserService();

const errors = ref<string[]>([]);
const email = ref("");
const password = ref("");
const confirmPassword = ref("");
const fName = ref("");
const lName = ref("");

async function registerClicked() {
  errors.value = [];

  if (email.value.length === 0 || email.value.length > 128) {
    errors.value.push("Email length should be in range of 1 and 128 characters");
  }

  if (password.value.length === 0 || password.value.length > 128) {
    errors.value.push("Password length should be in range of 1 and 128 characters");
  }

  if (fName.value.length === 0 || fName.value.length > 128) {
    errors.value.push("First name length should be in range of 1 and 128 characters");
  }

  if (lName.value.length === 0 || lName.value.length > 128) {
    errors.value.push("Last name length should be in range of 1 and 128 characters");
  }

  if (password.value !== confirmPassword.value) {
    errors.value.push("Passwords do not match to each other");
  }

  const validRegex = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;

  if (!email.value.match(validRegex)) {
    errors.value.push("Not proper email");
  }

  if (errors.value.length !== 0) {
    return;
  }

  const res = await appUserService.register(email.value, password.value, fName.value, lName.value);

  if (res.status === 400) {
    errors.value.push("Email is already registered");
  }

  if (errors.value.length !== 0) {
    return;
  }

  router.push("/");
}
</script>

<style scoped>

</style>
