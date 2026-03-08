<template>
  <header class="sticky top-0 z-50 bg-parchment-100/95 backdrop-blur-sm border-b border-accent-warm/40 shadow-book">
    <nav class="max-w-6xl mx-auto px-4 sm:px-6 lg:px-8">
      <div class="flex items-center justify-between h-16">
        <router-link
          to="/"
          class="font-display text-2xl font-bold text-ink-900 tracking-tight hover:text-accent-gold transition-colors"
        >
          Highlighted
        </router-link>

        <button
          type="button"
          class="md:hidden p-2 rounded-lg text-ink-800 hover:bg-parchment-200 transition-colors"
          @click="mobileMenuOpen = !mobileMenuOpen"
          aria-label="Toggle menu"
        >
          <svg class="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
            <path v-if="!mobileMenuOpen" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M4 6h16M4 12h16M4 18h16" />
            <path v-else stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M6 18L18 6M6 6l12 12" />
          </svg>
        </button>

        <div
          :class="[
            'md:flex md:items-center md:gap-8',
            mobileMenuOpen ? 'absolute top-16 left-0 right-0 bg-parchment-100 border-b border-accent-warm/40 py-4 px-4 flex flex-col gap-4' : 'hidden'
          ]"
        >
          <div class="flex flex-col md:flex-row md:items-center gap-4">
            <router-link
              to="/"
              class="font-body text-ink-800 hover:text-accent-gold font-medium transition-colors"
              @click="mobileMenuOpen = false"
            >
              Home
            </router-link>
            <router-link
              to="/books/index"
              class="font-body text-ink-800 hover:text-accent-gold font-medium transition-colors"
              @click="mobileMenuOpen = false"
            >
              Books
            </router-link>
            <router-link
              to="/authors"
              class="font-body text-ink-800 hover:text-accent-gold font-medium transition-colors"
              @click="mobileMenuOpen = false"
            >
              Authors
            </router-link>
          </div>

          <div class="flex flex-col md:flex-row md:items-center gap-4 pt-4 md:pt-0 border-t md:border-t-0 border-accent-warm/30">
            <template v-if="appUserStore.$state.jwt == null">
              <router-link
                to="/identity/register"
                class="font-body text-ink-800 hover:text-accent-gold font-medium transition-colors"
                @click="mobileMenuOpen = false"
              >
                Register
              </router-link>
              <router-link
                to="/identity/login"
                class="font-body font-medium px-4 py-2 rounded-lg bg-ink-900 text-parchment-100 hover:bg-ink-800 transition-colors"
                @click="mobileMenuOpen = false"
              >
                Login
              </router-link>
            </template>
            <template v-else>
              <button
                @click="appUserService.logout(); mobileMenuOpen = false"
                class="font-body text-ink-800 hover:text-red-600 font-medium transition-colors text-left md:text-center"
              >
                Logout
              </button>
            </template>
          </div>
        </div>
      </div>
    </nav>
  </header>
</template>

<script setup lang="ts">
import { ref } from "vue";
import { useAppUserStore } from "@/Stores/AppUserStore";
import { AppUserService } from "@/Services/AppUserService";

const appUserStore = useAppUserStore();
const appUserService = new AppUserService();
const mobileMenuOpen = ref(false);
</script>
