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
            <div class="m-2 col-4 form-floating">
              <textarea style="resize: none" v-model="book.content" class="form-control" type="text" placeholder="content"
                        id="content"></textarea>
              <label for="content">Content</label>
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
              <SearchBar :chosen-items="ChosenTags" :suggested-items="SuggestedTags"></SearchBar>
            </div>
          </div>

          <div class="row">
            <div class="m-2 col-1 form-group">
              <input @click="addBook()" type="submit" value="Create" class="btn btn-danger" />
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

<script setup lang="ts">
import { ref, reactive, onMounted } from "vue";
import { useRouter } from "vue-router";
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

const router = useRouter();
const langService = new LanguageService();
const tagsService = new TagService();
const bookService = new BookService();
const authorService = new AuthorService();

const errors = ref<string[]>([]);
const ChosenTags = ref<ITag[]>([]);
const SuggestedTags = ref<ITag[]>([]);
const languages = ref<ILanguage[]>([]);
const authors = ref<IAuthor[]>([]);

const book = reactive<IBook>({
  id: "",
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
});

async function addBook() {
  errors.value = [];

  if (book.title.length < 1 || book.title.length > 64) {
    errors.value.push("Title should be from 1 to 64 characters");
  }

  if (book.content.length < 1) {
    errors.value.push("Content cannot be empty");
  }

  if (!book.language) {
    errors.value.push("You have to choose language");
  }

  if (!book.author) {
    errors.value.push("You have to choose author");
  }

  if (errors.value.length !== 0) {
    document.scrollingElement!.scroll({ top: 0 });
    return;
  }

  book.symbolsTotal = book.content.length;
  await bookService.add({ ...book, tags: ChosenTags.value });

  router.push("/books");
}

onMounted(async () => {
  const tags = await tagsService.getAll();
  if (tags !== null) {
    SuggestedTags.value = tags;
  }

  const lan = await langService.getAll();
  if (lan !== null) {
    languages.value = lan;
  }

  const aut = await authorService.getAll();
  if (aut !== null) {
    authors.value = aut;
  }
});
</script>

<style scoped>

</style>
