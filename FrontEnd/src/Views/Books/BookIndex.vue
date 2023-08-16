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
      <div v-for="book in filteredList()">
        <Book :book="book"></Book>
      </div>
    </div>
  </div>

</template>


<script lang="ts">
import { Options, Vue } from "vue-class-component";
import { BookService } from "@/Services/BookService";
import { useAppUserStore } from "@/Stores/AppUserStore";
import type { IBook } from "@/Domain/IBook";
import TestBook from "@/Views/Books/testBook";
import Book from "@/components/Book.vue"

@Options({
  components: {Book}
})

export default class BookIndex extends Vue{
  bookService = new BookService();
  appUserStore = useAppUserStore();


  keywords = ""
  books: IBook[] = [];


  filteredList() {
    return this.books.filter((item) =>
      item.title.toLowerCase().includes(this.keywords.toLowerCase())
    );
  }

  async beforeCreate() {
    var res = await this.bookService.getAll();
    console.log(res);
    console.log(res);
    if (res === null){
      this.$router.push("/404")
    }
    else {
      this.books = res
    }
  }
};
</script>

<style>
</style>