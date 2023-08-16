<template>

  <div style="color: red" class="">
    ¡¡¡ {{message}} !!!
  </div>

</template>

<script lang="ts">
import { Options, Vue } from "vue-class-component";

@Options({
  props: {
    message: String,
  }
})

export default class ErrorMessage extends Vue {

}
</script>

<style scoped>
</style>