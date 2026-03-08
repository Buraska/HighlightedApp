<script setup lang="ts">
import Footer from "./components/Footer.vue";
import Header from "./components/Header.vue";
</script>
<template>
  <Header />
  <div  class="container">
    <main role="main" class="pb-3">
      <RouterView />
    </main>
  </div>
  <Footer />
</template>

<style scoped>
</style>
