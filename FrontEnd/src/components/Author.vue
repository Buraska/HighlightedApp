<template>
  <article class="group relative bg-white rounded-2xl border border-accent-warm/40 shadow-book hover:shadow-book-hover transition-all duration-300 overflow-hidden">
    <div class="h-1.5 bg-gradient-to-r from-accent-amber via-accent-gold to-accent-warm"></div>

    <div class="p-6">
      <router-link
        :to="'/authors/view/' + author.id"
        class="font-display text-xl font-semibold text-ink-900 hover:text-accent-gold mb-2 break-words block transition-colors"
      >
        {{ author.name }}
      </router-link>
      <p v-if="author.description" class="font-body text-sm text-ink-800/70 line-clamp-3 mb-4">
        {{ author.description }}
      </p>

      <div class="flex flex-wrap gap-2">
        <router-link
          :to="'/authors/view/' + author.id"
          class="inline-flex items-center gap-1.5 px-3 py-2 font-body text-sm font-medium text-ink-800 bg-parchment-100 hover:bg-parchment-200 rounded-lg transition-colors"
        >
          <i class="fa fa-book" aria-hidden="true"></i>
          View books
        </router-link>
        <router-link
          :to="'/authors/edit/' + author.id"
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
import type { IAuthor } from "../Domain/IAuthor";

defineProps<{
  author: IAuthor;
}>();
</script>
