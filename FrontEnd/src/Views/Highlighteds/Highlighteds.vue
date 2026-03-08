<template>
  <div class="max-w-6xl mx-auto px-4 sm:px-6 lg:px-8 py-12">
    <section class="text-center mb-16">
      <h1 class="font-display text-4xl sm:text-5xl font-bold text-ink-900 tracking-tight mb-4">
        Highlights
      </h1>
      <p class="font-body text-lg text-ink-800/80">
        Some things to think about.
      </p>
    </section>

    <section class="mb-12">
      <div class="relative group">
        <span class="absolute left-4 top-1/2 -translate-y-1/2 text-ink-800/50">
          <i class="fa fa-search" aria-hidden="true"></i>
        </span>
        <input
          v-model="keywords"
          type="text"
          placeholder="Search highlights..."
          class="w-full pl-12 pr-4 py-3.5 font-body text-ink-800 bg-white border border-accent-warm/60 rounded-xl shadow-book focus:outline-none focus:ring-2 focus:ring-accent-amber/50 focus:border-accent-amber transition-all placeholder:text-ink-800/40"
        />
      </div>
    </section>

    <section class="filter-result">
      <div v-if="filteredList.length > 0" class="space-y-4">
        <MyHighlighted v-for="highlighted in filteredList" :key="highlighted.id" :highlighted="highlighted" />
      </div>

      <div
        v-else
        class="text-center py-20 px-6 rounded-2xl bg-parchment-100/60 border border-dashed border-accent-warm/50"
      >
        <div class="inline-flex items-center justify-center w-20 h-20 rounded-full bg-accent-warm/30 mb-6">
          <i class="fa fa-highlighter text-3xl text-accent-amber" aria-hidden="true"></i>
        </div>
        <h2 class="font-display text-2xl font-semibold text-ink-900 mb-2">
          {{ keywords ? 'No highlights found' : 'No highlights yet' }}
        </h2>
        <p class="font-body text-ink-800/70">
          {{ keywords ? 'Try different keywords.' : 'Add highlights while reading your books.' }}
        </p>
      </div>
    </section>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from "vue";
import { useRoute } from "vue-router";
import MyHighlighted from "@/components/MyHighlighted.vue";
import type { IHighlighted } from "@/Domain/Highlighted";
import { BookService } from "@/Services/BookService";

const route = useRoute();
const bookService = new BookService();
const highlighteds = ref<IHighlighted[]>([]);
const keywords = ref("");

const filteredList = computed(() => {
  return highlighteds.value.filter((item) =>
    item.content.toLowerCase().includes(keywords.value.toLowerCase())
  );
});

onMounted(async () => {
  const id = route.params["bookId"].toString();
  const book = await bookService.get(id);
  highlighteds.value = book.highlighteds;
});
</script>
