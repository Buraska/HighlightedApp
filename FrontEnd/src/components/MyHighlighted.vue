<template>
  <!-- Quote -->
  <article
    v-if="highlighted.highlightedType.name === 'quote'"
    class="flex flex-col sm:flex-row gap-4 items-start sm:items-center p-6 rounded-2xl border-l-4 border-pink-200 bg-pink-50/50"
  >
    <p class="flex-1 font-body text-ink-800 italic">{{ highlighted.content }}</p>
    <div class="flex flex-wrap gap-2 shrink-0">
      <a
        href="#"
        @click.prevent="referenceHigh(highlighted.bookId, highlighted.startAt)"
        class="inline-flex items-center gap-1.5 px-3 py-2 font-body text-sm font-medium text-ink-800 bg-white border border-accent-warm/40 rounded-lg hover:bg-parchment-100 transition-colors"
      >
        Reference
      </a>
      <router-link
        :to="'/highlighted/edit/' + highlighted.id"
        class="inline-flex items-center gap-1.5 px-3 py-2 font-body text-sm font-medium text-ink-800 bg-white border border-accent-warm/40 rounded-lg hover:bg-parchment-100 transition-colors"
      >
        <i class="fa fa-pencil" aria-hidden="true"></i>
        Edit
      </router-link>
    </div>
  </article>

  <!-- Dictionary -->
  <article
    v-if="highlighted.highlightedType.name === 'dictionary'"
    class="flex flex-col sm:flex-row gap-4 p-6 rounded-2xl border-l-4 border-amber-300 bg-amber-50/50"
  >
    <div class="flex-1">
      <p class="font-body font-semibold text-ink-900 mb-2">{{ highlighted.content }}</p>
      <p class="font-body text-sm text-ink-800/80">{{ highlighted.comment }}</p>
    </div>
    <div class="flex flex-wrap gap-2 shrink-0">
      <a
        href="#"
        @click.prevent="referenceHigh(highlighted.bookId, highlighted.startAt)"
        class="inline-flex items-center gap-1.5 px-3 py-2 font-body text-sm font-medium text-ink-800 bg-white border border-accent-warm/40 rounded-lg hover:bg-parchment-100 transition-colors"
      >
        Reference
      </a>
      <router-link
        :to="'/highlighted/edit/' + highlighted.id"
        class="inline-flex items-center gap-1.5 px-3 py-2 font-body text-sm font-medium text-ink-800 bg-white border border-accent-warm/40 rounded-lg hover:bg-parchment-100 transition-colors"
      >
        <i class="fa fa-pencil" aria-hidden="true"></i>
        Edit
      </router-link>
    </div>
  </article>

  <!-- Note -->
  <article
    v-if="highlighted.highlightedType.name === 'note'"
    class="flex flex-col sm:flex-row gap-4 items-start sm:items-center p-6 rounded-2xl border-l-4 border-sky-300 bg-sky-50/50"
  >
    <p v-if="highlighted.comment" class="flex-1 font-body text-ink-800">{{ highlighted.comment }}</p>
    <div class="flex flex-wrap gap-2 shrink-0">
      <router-link
        :to="'/highlighted/edit/' + highlighted.id"
        class="inline-flex items-center gap-1.5 px-3 py-2 font-body text-sm font-medium text-ink-800 bg-white border border-accent-warm/40 rounded-lg hover:bg-parchment-100 transition-colors"
      >
        <i class="fa fa-pencil" aria-hidden="true"></i>
        Edit
      </router-link>
    </div>
  </article>
</template>

<script setup lang="ts">
import { useRouter } from "vue-router";
import { BookService } from "@/Services/BookService";
import type { IHighlighted } from "@/Domain/Highlighted";

defineProps<{
  highlighted: IHighlighted;
}>();

const router = useRouter();
const bookService = new BookService();

async function referenceHigh(bookId: string, symbNo: number) {
  const book = await bookService.get(bookId);

  book.currentSymbol = symbNo;
  await bookService.edit(book);

  router.push("/books/view/" + bookId);
}
</script>
