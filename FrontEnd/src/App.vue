 <script lang="ts">
import { Options, Vue } from "vue-class-component";
import { RouterLink, RouterView } from "vue-router";
import Footer from "./components/Footer.vue";
import Header from "./components/Header.vue";


export default {
  components: {
    Footer,
    Header
  },
}

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