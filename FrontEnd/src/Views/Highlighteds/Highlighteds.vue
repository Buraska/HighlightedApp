<template>

  <div class="container">
    <div class="row">
      <div class="text-center">
        <h3> Highlighted</h3>
        <p>Some things to think</p>
      </div>
    </div>

    <div class="row bg-secondary p-3">
      <div class="col input-group position-relative">
        <input v-model="keywords" type="text" class="form-control" placeholder="Enter Your Keywords" id="keywords">
      </div>

      <div class="col-6"></div>
      <div class="col">
      </div>
    </div>

    <div class="filter-result">
      <div v-for="highlighted in filteredList">
        <MyHighlighted :highlighted="highlighted"></MyHighlighted>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from "vue";
import { useRoute } from "vue-router";
import MyHighlighted from "@/components/MyHighlighted.vue";
import type { IHighlighted } from "@/Domain/Highlighted";
import { BookService } from "@/Services/BookService";

const route = useRoute();
const bookService = new BookService();
const highlighteds = ref<IHighlighted[]>([]);
const keywords = ref("");

const filteredList = computed(() => {
  return highlighteds.value.filter((item) =>
    item.content.toLowerCase().includes(keywords.value.toLowerCase())
  );
});

onMounted(async () => {
  const id = route.params["bookId"].toString();
  const book = await bookService.get(id);
  highlighteds.value = book.highlighteds;
});
</script>

<style scoped>

</style>
