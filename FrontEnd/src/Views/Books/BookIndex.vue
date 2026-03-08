<template>
  <div class="max-w-6xl mx-auto px-4 sm:px-6 lg:px-8 py-12">
    <!-- Hero Section -->
    <section class="text-center mb-16">
      <h1 class="font-display text-4xl sm:text-5xl lg:text-6xl font-bold text-ink-900 tracking-tight mb-4">
        Your Library
      </h1>
      <p class="font-body text-lg text-ink-800/80 max-w-2xl mx-auto">
        Read. Highlight. Remember. Every book tells a story—capture yours.
      </p>
    </section>

    <!-- Search & Add Bar -->
    <section class="mb-12">
      <div class="flex flex-col sm:flex-row gap-4 items-stretch sm:items-center">
        <div class="flex-1 relative group">
          <span class="absolute left-4 top-1/2 -translate-y-1/2 text-ink-800/50 group-focus-within:text-accent-amber transition-colors">
            <i class="fa fa-search" aria-hidden="true"></i>
          </span>
          <input
            v-model="keywords"
            type="text"
            placeholder="Search your books..."
            class="w-full pl-12 pr-4 py-3.5 font-body text-ink-800 bg-white border border-accent-warm/60 rounded-xl shadow-book focus:outline-none focus:ring-2 focus:ring-accent-amber/50 focus:border-accent-amber transition-all placeholder:text-ink-800/40"
          />
        </div>
        <router-link
          to="/books/add"
          class="inline-flex items-center justify-center gap-2 px-6 py-3.5 font-body font-semibold text-white bg-ink-900 hover:bg-ink-800 rounded-xl shadow-book hover:shadow-book-hover transition-all duration-200 shrink-0"
        >
          <i class="fa fa-plus" aria-hidden="true"></i>
          Add Book
        </router-link>
      </div>
    </section>

    <!-- Books Grid -->
    <section class="filter-result">
      <div
        v-if="filteredList.length > 0"
        class="grid gap-6 sm:grid-cols-2 lg:grid-cols-3"
      >
        <Book
          v-for="book in filteredList"
          :key="book.id"
          :book="book"
        />
      </div>

      <!-- Empty State -->
      <div
        v-else
        class="text-center py-20 px-6 rounded-2xl bg-parchment-100/60 border border-dashed border-accent-warm/50"
      >
        <div class="inline-flex items-center justify-center w-20 h-20 rounded-full bg-accent-warm/30 mb-6">
          <i class="fa fa-book text-3xl text-accent-amber" aria-hidden="true"></i>
        </div>
        <h2 class="font-display text-2xl font-semibold text-ink-900 mb-2">
          {{ keywords ? 'No books found' : 'No books yet' }}
        </h2>
        <p class="font-body text-ink-800/70 mb-6 max-w-md mx-auto">
          {{ keywords ? 'Try different keywords or add a new book to your library.' : 'Start your reading journey by adding your first book.' }}
        </p>
        <router-link
          v-if="!keywords"
          to="/books/add"
          class="inline-flex items-center gap-2 px-6 py-3 font-body font-semibold text-white bg-ink-900 hover:bg-ink-800 rounded-xl transition-colors"
        >
          <i class="fa fa-plus" aria-hidden="true"></i>
          Add Your First Book
        </router-link>
      </div>
    </section>
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
  if (res === null) {
    router.push("/404");
  } else {
    books.value = res;
  }
});
</script>
