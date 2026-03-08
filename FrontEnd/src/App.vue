<script setup lang="ts">
import Footer from "./components/Footer.vue";
import Header from "./components/Header.vue";
</script>
<template>
  <div class="min-h-screen flex flex-col bg-parchment-50">
    <Header />
    <main role="main" class="flex-1">
      <RouterView />
    </main>
    <Footer />
  </div>
</template>
