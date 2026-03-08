<template>
  <article
    class="group relative bg-white rounded-2xl border border-accent-warm/40 shadow-book hover:shadow-book-hover transition-all duration-300 overflow-hidden"
  >
    <div class="h-1.5 bg-gradient-to-r from-accent-amber via-accent-gold to-accent-warm"></div>

    <div class="p-6">
      <div class="mb-4">
        <h3 class="font-display text-xl font-semibold text-ink-900 mb-1 line-clamp-2">
          {{ book.title }}
        </h3>
        <p v-if="book.author" class="font-body text-sm text-ink-800/70">
          {{ book.author.name }}
        </p>
        <p v-else class="font-body text-sm text-ink-800/40 italic">
          No author
        </p>
      </div>

      <div class="flex flex-wrap gap-3 mb-5">
        <div class="inline-flex items-center gap-1.5 px-3 py-1.5 rounded-lg bg-parchment-100">
          <span class="w-2 h-2 rounded-full bg-accent-amber"></span>
          <span class="font-body text-xs font-medium text-ink-800">
            {{ book.symbolsTotal ? Math.round((book.currentSymbol / book.symbolsTotal) * 100) : 0 }}% read
          </span>
        </div>
        <div
          :class="[
            'inline-flex items-center gap-1.5 px-3 py-1.5 rounded-lg',
            book.isFinished ? 'bg-green-100 text-green-800' : 'bg-amber-50 text-amber-800'
          ]"
        >
          <i :class="['fa', book.isFinished ? 'fa-check-circle' : 'fa-circle-o']" aria-hidden="true"></i>
          <span class="font-body text-xs font-medium">
            {{ book.isFinished ? 'Finished' : 'In progress' }}
          </span>
        </div>
      </div>

      <div class="flex flex-wrap gap-2">
        <router-link
          :to="'/books/preview' + book.id"
          class="inline-flex items-center gap-1.5 px-3 py-2 font-body text-sm font-medium text-red-700 bg-red-50 hover:bg-red-100 rounded-lg transition-colors"
        >
          <i class="fa fa-book" aria-hidden="true"></i>
          Read
        </router-link>
        <router-link
          :to="'/highlighted/' + book.id"
          class="inline-flex items-center gap-1.5 px-3 py-2 font-body text-sm font-medium text-ink-800 bg-parchment-100 hover:bg-parchment-200 rounded-lg transition-colors"
        >
          <i class="fa fa-search" aria-hidden="true"></i>
          Highlights
        </router-link>
        <router-link
          :to="'/books/edit/' + book.id"
          class="inline-flex items-center gap-1.5 px-3 py-2 font-body text-sm font-medium text-ink-800 bg-parchment-100 hover:bg-parchment-200 rounded-lg transition-colors"
        >
          <i class="fa fa-pencil" aria-hidden="true"></i>
          Edit
        </router-link>
      </div>
    </div>
  </article>
</template>

<script setup lang="ts">
import type { IBook } from "@/Domain/IBook";

defineProps<{
  book: IBook;
}>();
</script>
