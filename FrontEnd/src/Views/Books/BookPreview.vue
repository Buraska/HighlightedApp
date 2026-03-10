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

      <div class="text-center mb-12">
        <router-link :to="'/books/view/' + book.id" class="btn-primary">
          <i class="fa fa-book mr-2" aria-hidden="true"></i>
          Read
        </router-link>
      </div>

      <!-- Tabs: Notes & Highlights -->
      <section v-if="book.highlighteds?.length" class="border-t border-accent-warm/40 pt-8">
        <div class="flex gap-1 mb-6 border-b border-accent-warm/40">
          <button
            @click="activeTab = 'notes'"
            :class="[
              'px-6 py-3 font-body font-medium transition-colors border-b-2 -mb-px',
              activeTab === 'notes'
                ? 'border-accent-amber text-ink-900'
                : 'border-transparent text-ink-800/60 hover:text-ink-800'
            ]"
          >
            Notes ({{ notes.length }})
          </button>
          <button
            @click="activeTab = 'highlights'"
            :class="[
              'px-6 py-3 font-body font-medium transition-colors border-b-2 -mb-px',
              activeTab === 'highlights'
                ? 'border-accent-amber text-ink-900'
                : 'border-transparent text-ink-800/60 hover:text-ink-800'
            ]"
          >
            Highlights ({{ highlights.length }})
          </button>
        </div>

        <!-- Notes tab -->
        <div v-show="activeTab === 'notes'" class="space-y-4">
          <div
            v-for="highlighted in notes"
            :key="highlighted.id"
            class="relative group"
          >
            <MyHighlighted :highlighted="highlighted" />
      
          </div>
          <p v-if="notes.length === 0" class="font-body text-ink-800/60 py-8 text-center">
            No notes yet. Add notes while reading.
          </p>
        </div>

        <!-- Highlights tab -->
        <div v-show="activeTab === 'highlights'" class="space-y-4">
          <div
            v-for="highlighted in highlights"
            :key="highlighted.id"
            class="relative group"
          >
            <MyHighlighted :highlighted="highlighted" />
      
          </div>
          <p v-if="highlights.length === 0" class="font-body text-ink-800/60 py-8 text-center">
            No highlights yet. Add quotes or dictionary entries while reading.
          </p>
        </div>
      </section>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from "vue";
import { useRoute } from "vue-router";
import type { IBook } from "@/Domain/IBook";
import type { IHighlighted } from "@/Domain/Highlighted";
import { BookService } from "@/Services/BookService";
import { HighlightedsService } from "@/Services/Highlighteds";
import MyHighlighted from "@/components/MyHighlighted.vue";

const route = useRoute();
const bookService = new BookService();
const highlightedsService = new HighlightedsService();

const book = ref<IBook>({ currentSymbol: 0, highlighteds: [], isFinished: false, symbolsTotal: 0, title: "", content: "" });
const activeTab = ref<"notes" | "highlights">("notes");

const notes = computed(() =>
  book.value.highlighteds?.filter((h) => h.highlightedType.name === "note") ?? []
);

const highlights = computed(() =>
  book.value.highlighteds?.filter((h) =>
    h.highlightedType.name === "quote" || h.highlightedType.name === "dictionary"
  ) ?? []
);

async function removeHighlighted(highlighted: IHighlighted) {
  if (!highlighted.id) return;
  await highlightedsService.delete(highlighted.id);
  book.value.highlighteds = book.value.highlighteds?.filter((h) => h.id !== highlighted.id) ?? [];
}

onMounted(async () => {
  const id = route.params["id"].toString();
  book.value = await bookService.get(id);
});
</script>
