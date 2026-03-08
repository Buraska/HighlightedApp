<template>

  <div class="container">
    <div class="row">
      <div class="text-center">
        <h3> Books</h3>
        <p>Read Write Side.</p>
      </div>
    </div>

    <div class="row bg-secondary p-3">
      <div class="col input-group position-relative">
        <input v-model="keywords" type="text" class="form-control" placeholder="Enter Your Keywords" id="keywords">
      </div>

      <div class="col-6"></div>
      <div class="col">
        <router-link to="/books/add">
          <button style="float: right" type="button" class="btn  btn-block btn-danger btn-custom" id="add-book">
            Add book
          </button>
        </router-link>
      </div>
    </div>

    <div class="filter-result">
      <div v-for="book in filteredList">
        <Book :book="book" />
      </div>
    </div>
  </div>

</template>

<script setup lang="ts">
import { ref, computed, onMounted } from "vue";
import { useRouter } from "vue-router";
import { BookService } from "@/Services/BookService";
import type { IBook } from "@/Domain/IBook";
import Book from "@/components/Book.vue";

const router = useRouter();
const bookService = new BookService();
const keywords = ref("");
const books = ref<IBook[]>([]);

const filteredList = computed(() => {
  return books.value.filter((item) =>
    item.title.toLowerCase().includes(keywords.value.toLowerCase())
  );
});

onMounted(async () => {
  const res = await bookService.getAll();
  console.log(res);
  if (res === null) {
    router.push("/404");
  } else {
    books.value = res;
  }
});
</script>

<style>
</style>
