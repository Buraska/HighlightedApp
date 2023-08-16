<template>
  <div class="container">
    <div class="row">
      <div class="text-center">
        <h3> Books</h3>
        <p>Create</p>
      </div>
    </div>
  </div>

  <hr />

  <div style="">

    <div class="row">
      <div class="col">
        <div v-if="errors.length !== 0" class="text-danger validation-summary-errors" data-valmsg-summary="true">
          <div v-for="error in errors">
            <ErrorMessage :message="error"></ErrorMessage>
          </div>
        </div>

        <div class="container" style="border-bottom: 5px pink inset; align-items: center;">

          <div class="row">
            <div class="m-2  col-4 form-floating">
              <textarea style="resize: none" v-model="book.title" class="form-control" type="text" placeholder="title"
                        id="title"></textarea>
              <label for="title">Title</label>
            </div>
          </div>

          <div class="row">
            <div class="m-2 col-4 form-floating">
              <select v-model="book.language" class="form-select" id="lang">
                <option v-for="lang in languages" :value="lang">{{ lang.name }}</option>
              </select>
              <label for="lang">Language</label>
            </div>
          </div>

          <div class="row">
            <div class="col-2 form-floating m-2">
              <select v-model="book.author" class="form-select" id="authorSelect">
                <option v-for="author in authors" :value="author">{{ author.name }}</option>
              </select>
              <label for="authorSelect">Author</label>
            </div>
            <div style="margin-top: 20px" class="col-2">
              <router-link to="/authors/add">
                <button class="btn-secondary">Add new</button>
              </router-link>
            </div>
          </div>

          <div class="row">
            <div class="m-2 col-6 form-group">
              <SearchBar :chosen-items="book.tags" :suggested-items="SuggestedTags"></SearchBar>
            </div>
          </div>

          <div class="row">
            <div class="m-2 col-1 form-group">
              <input @click="editBook()" type="submit" value="Edit" class="btn btn-danger" />
            </div>
          </div>


        </div>
      </div>
    </div>
  </div>

  <div>
    <router-link class="text-danger" to="/books">Back to List</router-link>
  </div>
</template>

<script lang="ts">
import { Options, Vue } from "vue-class-component";
import SearchBar from "@/components/SearchBar.vue";
import { BookService } from "@/Services/BookService";
import { LanguageService } from "@/Services/LanguageService";
import { TagService } from "@/Services/TagService";
import type { ITag } from "@/Domain/ITag";
import type { ILanguage } from "@/Domain/ILanguage";
import { AuthorService } from "@/Services/AuthorService";
import type { IAuthor } from "@/Domain/IAuthor";
import ErrorMessage from "@/components/ErrorMessage.vue";
import type { IBook } from "@/Domain/IBook";

@Options({
  components: { SearchBar, ErrorMessage }
})

export default class BookEdit extends Vue {
  langService = new LanguageService();
  tagsService = new TagService();
  bookService = new BookService();
  authorService = new AuthorService();

  errors: string[] = [];

  SuggestedTags: ITag[] = [];
  languages: ILanguage[] = [];
  authors: IAuthor[] = [];

  book: IBook = {
    author: undefined,
    content: "",
    currentSymbol: 0,
    highlighteds: [],
    isFinished: false,
    language: undefined,
    preference: undefined,
    symbolsTotal: 0,
    tags: [],
    title: ""
  }

  async beforeCreate() {
    var id = this.$route.params["id"].toString();
    this.book = await this.bookService.get(id);

    console.log(this.book);

    var tags = await this.tagsService.getAll();
    if (tags !== null) {
      this.SuggestedTags = tags.filter((x) => this.book.tags?.map((x) => x.name).indexOf(x.name) === -1)
    }

  }

  async editBook() {
    this.errors = [];

    if (this.book.title.length < 1 || this.book.title.length > 64) {
      this.errors.push("Title should be from 1 to 64 characters");
    }

    if (!this.book.language) {
      this.errors.push("You have to choose language");
    }

    if (!this.book.author) {
      this.errors.push("You have to choose author");
    }

    if (this.errors.length !== 0) {
      document.scrollingElement!.scroll({ top: 0 });
      return;
    }


    var res = await this.bookService.edit(this.book);

    this.$router.push("/books")
  }

  async mounted() {

    var lan = await this.langService.getAll();
    if (lan !== null) {
      this.languages = lan;
    }

    var aut = await this.authorService.getAll();
    if (aut !== null) {
      this.authors = aut;
    }
  }


};
</script>

<style scoped>

</style>