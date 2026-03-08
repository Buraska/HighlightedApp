<template>
  <div class="max-w-6xl mx-auto px-4 sm:px-6 lg:px-8 py-12">
    <div class="max-w-md mx-auto">
      <h1 class="font-display text-3xl font-bold text-ink-900 mb-2">Login</h1>
      <p class="font-body text-ink-800/70 mb-4">Welcome back to your library.</p>
      <p class="font-body text-sm text-ink-800/60 mb-8 p-3 rounded-lg bg-parchment-100 border border-accent-warm/40">
        Demo: use email <code class="font-mono text-accent-amber">test@test.com</code> and password <code class="font-mono text-accent-amber">test</code>
      </p>

      <div v-if="errorHappened" class="mb-6 p-4 rounded-xl bg-red-50 border border-red-200 text-red-700 font-body text-sm">
        <ul class="list-disc list-inside">
          <li>{{ errors }}</li>
        </ul>
      </div>

      <form class="space-y-6" @submit.prevent="loginClicked()">
        <div>
          <label class="block font-body text-sm font-medium text-ink-800 mb-2">Email</label>
          <input
            v-model="email"
            type="email"
            class="input-field"
            placeholder="you@example.com"
          />
        </div>
        <div>
          <label class="block font-body text-sm font-medium text-ink-800 mb-2">Password</label>
          <input
            v-model="password"
            type="password"
            class="input-field"
            placeholder="••••••••"
          />
        </div>
        <button
          type="submit"
          class="w-full py-3 px-4 font-body font-semibold text-white bg-ink-900 hover:bg-ink-800 rounded-xl transition-colors"
        >
          Login
        </button>
      </form>
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

const email = ref("test@test.com");
const password = ref("test");
const errorHappened = ref(false);
const errors = ref("");

async function loginClicked() {
  const res = await service.login(email.value, password.value);

  if (appUserStore.$state.jwt) {
    router.push("/");
  }
}
</script>
