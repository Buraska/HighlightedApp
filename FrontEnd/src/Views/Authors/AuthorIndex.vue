<template>

  <div class="container">
    <div class="row">
      <div class="text-center">
        <h3> Authors</h3>
        <p>Read Write Create.</p>
      </div>
    </div>

    <div class="row bg-secondary p-3">
      <div class="col input-group position-relative">
        <input v-model="keywords" type="text" class="form-control" placeholder="Enter Your Keywords" id="keywords">
      </div>

      <div class="col-6"></div>
      <div class="col">
        <router-link to="/authors/add">
          <button style="float: right" type="button" class="btn  btn-block btn-danger btn-custom" id="add-book">
            Add author
          </button>
        </router-link>
      </div>
    </div>

    <div class="filter-result">
      <div v-for="author in filteredList()">
        <Author :author="author"></Author>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { Options, Vue } from "vue-class-component";
import { AuthorService } from "@/Services/AuthorService";
import type { IAuthor } from "@/Domain/IAuthor";
import Author from "@/components/Author.vue";
import SearchBar from "@/components/SearchBar.vue";

@Options({
  components: { Author, SearchBar }
})
export default class AuthorIndex extends Vue {

  authorService = new AuthorService();
  authors: IAuthor[] = [];

  keywords = "";

  filteredList() {
    return this.authors.filter((item) =>
      item.name.toLowerCase().includes(this.keywords.toLowerCase())
    );
  }

  async beforeCreate() {
      var res = await this.authorService.getAll();
      console.log(res);
      console.log(res);
      if (res === null){
        this.$router.push("/404")
      }
      else {
        this.authors = res
      }
  }
};

</script>

<style scoped>

</style>