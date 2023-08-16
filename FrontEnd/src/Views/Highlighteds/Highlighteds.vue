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
      <div v-for="highlighted in filteredList()">
        <MyHighlighted :highlighted="highlighted"></MyHighlighted>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { Options, Vue } from "vue-class-component";
import TestBook from "@/Views/Books/testBook";
import MyHighlighted from "@/components/MyHighlighted.vue";
import type { IHighlighted } from "@/Domain/Highlighted";
import { BookService } from "@/Services/BookService";



@Options({
  components: {MyHighlighted}
})
export default class Highlighteds extends Vue {

  highlighteds: IHighlighted[] = [];
  bookService = new BookService();

  keywords = "";

  async beforeCreate(){
    var id = this.$route.params["bookId"].toString();
    this.highlighteds = (await this.bookService.get(id)).highlighteds;
  }

  filteredList() {
    return this.highlighteds.filter((item) =>
      item.content.toLowerCase().includes(this.keywords.toLowerCase())
    );
  }

};

</script>

<style scoped>

</style>