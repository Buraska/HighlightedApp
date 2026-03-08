<template>
  <div class="max-w-4xl mx-auto px-4 sm:px-6 lg:px-8 py-12">
    <div class="bg-white rounded-2xl border border-accent-warm/40 shadow-book p-8 sm:p-12">
      <h1 class="font-display text-2xl sm:text-3xl font-bold text-ink-900 text-center mb-6">
        {{ book.title }}
      </h1>

      <p v-if="book.author" class="mb-6 font-body text-ink-800/80">
        by
        <router-link
          :to="'/authors/view/' + book.author.id"
          class="text-accent-amber hover:text-accent-gold font-medium transition-colors"
        >
          {{ book.author.name }}
        </router-link>
      </p>

      <div class="flex flex-wrap gap-4 justify-center mb-8">
        <div class="inline-flex items-center gap-2 px-4 py-2 rounded-lg bg-parchment-100">
          <span class="font-body text-sm text-ink-800">
            Progress: {{ book.symbolsTotal ? Math.round((book.currentSymbol / book.symbolsTotal) * 100) : 0 }}%
          </span>
        </div>
        <div
          :class="[
            'inline-flex items-center gap-2 px-4 py-2 rounded-lg',
            book.isFinished ? 'bg-green-100 text-green-800' : 'bg-amber-50 text-amber-800'
          ]"
        >
          <span class="font-body text-sm font-medium">
            {{ book.isFinished ? 'Finished' : 'In progress' }}
          </span>
        </div>
      </div>

      <div class="text-center">
        <router-link :to="'/books/view/' + book.id" class="btn-primary">
          <i class="fa fa-book mr-2" aria-hidden="true"></i>
          Read
        </router-link>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from "vue";
import { useRoute } from "vue-router";
import type { IBook } from "@/Domain/IBook";
import Author from "@/components/Author.vue";
import { BookService } from "@/Services/BookService";

const route = useRoute();
const bookService = new BookService();

const book = ref<IBook>({ currentSymbol: 0, highlighteds: [], isFinished: false, symbolsTotal: 0, title: "", content: "" });

onMounted(async () => {
  const id = route.params["id"].toString();
  book.value = await bookService.get(id);
});
</script>
