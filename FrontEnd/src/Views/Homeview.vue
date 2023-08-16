<template>
  HOMMMMMIEVIEW
</template>

<script lang="ts">
import { Options, Vue } from "vue-class-component";
import SearchBar from "@/components/SearchBar.vue";

@Options({
  components:{
    SearchBar
  }
})

export default class Homeview extends Vue {

  items: string[] = ["aaa", "bbb", "asdsadas"];
  chosenItems: string[] = []
}
</script>

<style scoped>
</style>