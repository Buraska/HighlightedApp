<template>
  <div class="max-w-6xl mx-auto px-4 sm:px-6 lg:px-8 py-12">
    <section class="text-center mb-16">
      <h1 class="font-display text-4xl sm:text-5xl font-bold text-ink-900 tracking-tight mb-4">
        Authors
      </h1>
      <p class="font-body text-lg text-ink-800/80">
        Read. Write. Create.
      </p>
    </section>

    <section class="mb-12">
      <div class="flex flex-col sm:flex-row gap-4 items-stretch sm:items-center">
        <div class="flex-1 relative group">
          <span class="absolute left-4 top-1/2 -translate-y-1/2 text-ink-800/50">
            <i class="fa fa-search" aria-hidden="true"></i>
          </span>
          <input
            v-model="keywords"
            type="text"
            placeholder="Search authors..."
            class="w-full pl-12 pr-4 py-3.5 font-body text-ink-800 bg-white border border-accent-warm/60 rounded-xl shadow-book focus:outline-none focus:ring-2 focus:ring-accent-amber/50 focus:border-accent-amber transition-all placeholder:text-ink-800/40"
          />
        </div>
        <router-link
          to="/authors/add"
          class="inline-flex items-center justify-center gap-2 px-6 py-3.5 font-body font-semibold text-white bg-ink-900 hover:bg-ink-800 rounded-xl shadow-book hover:shadow-book-hover transition-all duration-200 shrink-0"
        >
          <i class="fa fa-plus" aria-hidden="true"></i>
          Add Author
        </router-link>
      </div>
    </section>

    <section class="filter-result">
      <div v-if="filteredList.length > 0" class="grid gap-6 sm:grid-cols-2 lg:grid-cols-3">
        <Author v-for="author in filteredList" :key="author.id" :author="author" />
      </div>

      <div
        v-else
        class="text-center py-20 px-6 rounded-2xl bg-parchment-100/60 border border-dashed border-accent-warm/50"
      >
        <div class="inline-flex items-center justify-center w-20 h-20 rounded-full bg-accent-warm/30 mb-6">
          <i class="fa fa-user text-3xl text-accent-amber" aria-hidden="true"></i>
        </div>
        <h2 class="font-display text-2xl font-semibold text-ink-900 mb-2">
          {{ keywords ? 'No authors found' : 'No authors yet' }}
        </h2>
        <p class="font-body text-ink-800/70 mb-6 max-w-md mx-auto">
          {{ keywords ? 'Try different keywords or add a new author.' : 'Start by adding your first author.' }}
        </p>
        <router-link
          v-if="!keywords"
          to="/authors/add"
          class="inline-flex items-center gap-2 px-6 py-3 font-body font-semibold text-white bg-ink-900 hover:bg-ink-800 rounded-xl transition-colors"
        >
          <i class="fa fa-plus" aria-hidden="true"></i>
          Add Your First Author
        </router-link>
      </div>
    </section>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from "vue";
import { useRouter } from "vue-router";
import { AuthorService } from "@/Services/AuthorService";
import type { IAuthor } from "@/Domain/IAuthor";
import Author from "@/components/Author.vue";

const router = useRouter();
const authorService = new AuthorService();
const authors = ref<IAuthor[]>([]);
const keywords = ref("");

const filteredList = computed(() => {
  return authors.value.filter((item) =>
    item.name.toLowerCase().includes(keywords.value.toLowerCase())
  );
});

onMounted(async () => {
  const res = await authorService.getAll();
  if (res === null) {
    router.push("/404");
  } else {
    authors.value = res;
  }
});
</script>
