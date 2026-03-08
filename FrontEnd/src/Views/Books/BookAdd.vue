<template>
  <div class="max-w-6xl mx-auto px-4 sm:px-6 lg:px-8 py-12">
    <section class="text-center mb-12">
      <h1 class="font-display text-3xl font-bold text-ink-900 mb-2">Add Book</h1>
      <p class="font-body text-ink-800/70">Create a new book in your library.</p>
    </section>

    <div v-if="errors.length !== 0" class="mb-6 p-4 rounded-xl bg-red-50 border border-red-200 space-y-2">
      <ErrorMessage v-for="error in errors" :key="error" :message="error" />
    </div>

    <div class="bg-white rounded-2xl border border-accent-warm/40 shadow-book p-8 mb-8">
      <div class="space-y-6">
        <div>
          <label for="title" class="block font-body text-sm font-medium text-ink-800 mb-2">Title</label>
          <textarea
            id="title"
            v-model="book.title"
            rows="2"
            class="input-field resize-none"
            placeholder="Book title"
          ></textarea>
        </div>

        <div>
          <label for="lang" class="block font-body text-sm font-medium text-ink-800 mb-2">Language</label>
          <select id="lang" v-model="book.language" class="input-field">
            <option v-for="lang in languages" :key="lang.name" :value="lang">{{ lang.name }}</option>
          </select>
        </div>

        <div>
          <label for="content" class="block font-body text-sm font-medium text-ink-800 mb-2">Content</label>
          <textarea
            id="content"
            v-model="book.content"
            rows="6"
            class="input-field resize-none"
            placeholder="Paste or type book content..."
          ></textarea>
        </div>

        <div class="flex flex-wrap gap-4 items-end">
          <div class="flex-1 min-w-[200px]">
            <label for="authorSelect" class="block font-body text-sm font-medium text-ink-800 mb-2">Author</label>
            <select id="authorSelect" v-model="book.author" class="input-field">
              <option v-for="author in authors" :key="author.id" :value="author">{{ author.name }}</option>
            </select>
          </div>
          <router-link to="/authors/add" class="btn-secondary shrink-0">
            Add new author
          </router-link>
        </div>

        <div>
          <SearchBar :chosen-items="ChosenTags" :suggested-items="SuggestedTags" />
        </div>

        <div class="flex gap-4 pt-4">
          <button @click="addBook()" type="button" class="btn-danger">
            Create Book
          </button>
          <router-link to="/books" class="btn-secondary">
            Cancel
          </router-link>
        </div>
      </div>
    </div>

    <router-link to="/books" class="font-body text-accent-amber hover:text-accent-gold font-medium transition-colors">
      ← Back to List
    </router-link>
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
