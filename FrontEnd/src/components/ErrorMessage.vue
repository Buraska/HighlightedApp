<template>
  <p class="font-body text-sm text-red-700">
    {{ message }}
  </p>
</template>

<script setup lang="ts">
defineProps<{
  message: string;
}>();
</script>
