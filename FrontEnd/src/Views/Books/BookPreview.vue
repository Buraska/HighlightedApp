<template>
  <div class="row">
    <div class="col-2"></div>
    <div class="col-8 bg-light">
      <div class="row text-center m-2"><h3>{{ book.title }}</h3></div>
      <hr/>

      <div class="row"><h3></h3>
        <Author v-if="book.author" :author="book.author" />
      </div>

      <div class="row"><h3>process: {{ Math.round((book.currentSymbol / book.symbolsTotal) * 100) }}%</h3></div>
      <div class="row"><h3>finished: {{ book.isFinished }}</h3></div>

      <div class="row text-center">
        <router-link :to="'/books/view/'+book.id"><button style="width: 100px;" class="btn-danger">read</button></router-link>
      </div>


    </div>
    <div class="col-2"></div>
    <div></div>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from "vue";
import { useRoute } from "vue-router";
import type { IBook } from "@/Domain/IBook";
import Author from "@/components/Author.vue";
import { BookService } from "@/Services/BookService";

const route = useRoute();
const bookService = new BookService();

const book = ref<IBook>({ currentSymbol: 0, highlighteds: [], isFinished: false, symbolsTotal: 0, title: "", content: "" });

onMounted(async () => {
  const id = route.params["id"].toString();
  book.value = await bookService.get(id);
});
</script>

<style scoped>

</style>
