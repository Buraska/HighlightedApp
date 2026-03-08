<template>
  <div class="max-w-2xl mx-auto px-4 sm:px-6 lg:px-8 py-24 text-center">
    <div class="inline-flex items-center justify-center w-24 h-24 rounded-full bg-red-100 mb-8">
      <i class="fa fa-exclamation-triangle text-4xl text-red-600" aria-hidden="true"></i>
    </div>
    <h1 class="font-display text-4xl font-bold text-ink-900 mb-4">Bad Request</h1>
    <p class="font-body text-lg text-ink-800/70 mb-8">
      The page you're looking for doesn't exist or you don't have access to it.
    </p>
    <router-link to="/" class="btn-primary">
      <i class="fa fa-home mr-2" aria-hidden="true"></i>
      Back to Home
    </router-link>
  </div>
</template>

<script setup lang="ts">
</script>
