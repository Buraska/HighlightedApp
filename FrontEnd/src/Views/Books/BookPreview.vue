<template>
  <div class="row">
    <div class="col-2"></div>
    <div class="col-8 bg-light">
      <div class="row text-center m-2"><h3>{{ book.title }}</h3></div>
      <hr/>

      <div class="row"><h3></h3>
        <Author :author="book.author"></Author>
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

<script lang="ts">
import { Options, Vue } from "vue-class-component";
import type { IBook } from "@/Domain/IBook";
import TestBook from "@/Views/Books/testBook";
import Author from "@/components/Author.vue";
import { BookService } from "@/Services/BookService";

@Options({
    components: { Author }
  }
)

export default class BookPreview extends Vue {
  book: IBook = {currentSymbol: 0, highlighteds: [], isFinished: false, symbolsTotal: 0, title: "", content:""};
  bookService = new BookService();


  async beforeCreate() {
    var id = this.$route.params["id"].toString();
    this.book = await this.bookService.get(id);
  }

  };
</script>

<style scoped>

</style>