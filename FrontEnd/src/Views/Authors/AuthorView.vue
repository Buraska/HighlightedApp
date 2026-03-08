<template>
  <div class="max-w-6xl mx-auto px-4 sm:px-6 lg:px-8 py-12">
    <div v-if="author" class="mb-12">
      <h1 class="font-display text-3xl sm:text-4xl font-bold text-ink-900 mb-2">
        {{ author.name }}
      </h1>
      <p v-if="author.description" class="font-body text-ink-800/80">
        {{ author.description }}
      </p>
    </div>

    <section>
      <h2 class="font-display text-xl font-semibold text-ink-900 mb-6">Books</h2>
      <div v-if="authorBooks.length > 0" class="grid gap-6 sm:grid-cols-2 lg:grid-cols-3">
        <Book v-for="book in authorBooks" :key="book.id" :book="book" />
      </div>
      <div
        v-else
        class="text-center py-16 px-6 rounded-2xl bg-parchment-100/60 border border-dashed border-accent-warm/50"
      >
        <p class="font-body text-ink-800/70">No books by this author yet.</p>
        <router-link to="/books/add" class="inline-flex items-center gap-2 mt-4 font-body text-accent-amber hover:text-accent-gold font-medium">
          <i class="fa fa-plus" aria-hidden="true"></i>
          Add a book
        </router-link>
      </div>
    </section>

    <router-link to="/authors" class="inline-block mt-8 font-body text-accent-amber hover:text-accent-gold font-medium transition-colors">
      ← Back to Authors
    </router-link>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from "vue";
import { useRoute, useRouter } from "vue-router";
import { AuthorService } from "@/Services/AuthorService";
import { BookService } from "@/Services/BookService";
import type { IAuthor } from "@/Domain/IAuthor";
import type { IBook } from "@/Domain/IBook";
import Book from "@/components/Book.vue";

const route = useRoute();
const router = useRouter();
const authorService = new AuthorService();
const bookService = new BookService();

const author = ref<IAuthor | null>(null);
const allBooks = ref<IBook[]>([]);

const authorBooks = computed(() => {
  if (!author.value?.id) return [];
  return allBooks.value.filter((b) => b.author?.id === author.value?.id);
});

onMounted(async () => {
  const id = route.params["id"].toString();
  author.value = await authorService.get(id);
  if (!author.value) {
    router.push("/authors");
    return;
  }
  const books = await bookService.getAll();
  allBooks.value = books ?? [];
});
</script>
