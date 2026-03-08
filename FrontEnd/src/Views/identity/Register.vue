<template>
  <div class="max-w-6xl mx-auto px-4 sm:px-6 lg:px-8 py-12">
    <div class="max-w-2xl mx-auto">
      <h1 class="font-display text-3xl font-bold text-ink-900 mb-2">Register</h1>
      <p class="font-body text-ink-800/70 mb-8">Create a new account.</p>

      <div v-if="errors.length !== 0" class="mb-6 p-4 rounded-xl bg-red-50 border border-red-200 space-y-2">
        <ErrorMessage v-for="error in errors" :key="error" :message="error" />
      </div>

      <form class="space-y-6" @submit.prevent="registerClicked">
        <div class="grid sm:grid-cols-2 gap-6">
          <div class="space-y-6">
            <div>
              <label for="Input_Email" class="block font-body text-sm font-medium text-ink-800 mb-2">Email</label>
              <input
                id="Input_Email"
                v-model="email"
                type="email"
                class="input-field"
                placeholder="you@example.com"
              />
            </div>
            <div>
              <label for="Input_Password" class="block font-body text-sm font-medium text-ink-800 mb-2">Password</label>
              <input
                id="Input_Password"
                v-model="password"
                type="password"
                class="input-field"
                placeholder="••••••••"
              />
            </div>
            <div>
              <label for="Input_ConfirmPassword" class="block font-body text-sm font-medium text-ink-800 mb-2">Confirm password</label>
              <input
                id="Input_ConfirmPassword"
                v-model="confirmPassword"
                type="password"
                class="input-field"
                placeholder="••••••••"
              />
            </div>
            <button
              type="submit"
              class="w-full py-3 px-4 font-body font-semibold text-white bg-ink-900 hover:bg-ink-800 rounded-xl transition-colors"
            >
              Register
            </button>
          </div>
          <div class="space-y-6">
            <div>
              <label for="Input_fName" class="block font-body text-sm font-medium text-ink-800 mb-2">First Name</label>
              <input
                id="Input_fName"
                v-model="fName"
                type="text"
                class="input-field"
                placeholder="John"
              />
            </div>
            <div>
              <label for="Input_lName" class="block font-body text-sm font-medium text-ink-800 mb-2">Last Name</label>
              <input
                id="Input_lName"
                v-model="lName"
                type="text"
                class="input-field"
                placeholder="Doe"
              />
            </div>
          </div>
        </div>
      </form>
    </div>
  </div>
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
