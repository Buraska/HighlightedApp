<template>

  <div style="color: red" class="">
    ¡¡¡ {{ message }} !!!
  </div>

</template>

<script setup lang="ts">
defineProps<{
  message: string;
}>();
</script>

<style scoped>
</style>
