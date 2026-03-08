<template>

  <div v-if="highlighted.highlightedType.name === 'quote'" :id="highlighted.id" style="display: flex; padding: 5px; border: 5px pink inset; align-items: center; margin: 20px" class="row bg-body align-content-center">
    <div style="" class="col">
      <h6>{{ highlighted.content }}</h6>
    </div>

    <div class="col-2">
      <div class="dropdown">
        <button style="" class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton1" data-bs-toggle="dropdown" aria-expanded="false">
          action
        </button>
        <ul class="dropdown-menu" aria-labelledby="dropdownMenuButton1">
          <li><a v-on:click="referenceHigh(highlighted.bookId, highlighted.startAt)" class="dropdown-item" href="#">reference</a></li>
          <li> <router-link class="dropdown-item" :to="'/highlighted/edit/' + highlighted.id">edit</router-link></li>
        </ul>
      </div>
    </div>

  </div>

  <div v-if="highlighted.highlightedType.name === 'dictionary'" style="display: flex; padding: 5px; border: 5px burlywood inset; align-items: center; margin: 20px" class="row bg-body align-content-center">
    <div style="" class="col-5">
      <h6>{{ highlighted.content }}</h6>
    </div>

    <div  style="border-left-style: solid; border-color: darkgray" class="col-5">
      {{ highlighted.comment }}
    </div>

    <div class="col-1">
      <div class="dropdown">
        <button style="" class="btn btn-secondary dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false">
          action
        </button>
        <ul class="dropdown-menu" aria-labelledby="dropdownMenuButton1">
          <li><a v-on:click="referenceHigh(highlighted.bookId, highlighted.startAt)" class="dropdown-item" href="#">reference</a></li>
          <li> <router-link class="dropdown-item" :to="'/highlighted/edit/'+ highlighted.id">edit</router-link></li>
        </ul>
      </div>
    </div>
  </div>

  <div v-if="highlighted.highlightedType.name === 'note'" style="display: flex; padding: 5px; border: 5px deepskyblue inset; align-items: center; margin: 20px" class="row bg-body align-content-center">
    <div v-if="highlighted.comment" style="" class="col-10">
      {{ highlighted.comment }}
    </div>

    <div class="col-1">
      <div class="dropdown">
        <button style="" class="btn btn-secondary dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false">
          action
        </button>
        <ul class="dropdown-menu" aria-labelledby="dropdownMenuButton1">
          <li> <router-link class="dropdown-item" :to="'/highlighted/edit/' + highlighted.id">
            edit
          </router-link></li>
        </ul>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { useRouter } from "vue-router";
import { HighlightedsService } from "@/Services/Highlighteds";
import { BookService } from "@/Services/BookService";
import type { IHighlighted } from "@/Domain/Highlighted";

defineProps<{
  highlighted: IHighlighted;
}>();

const router = useRouter();
const highLightService = new HighlightedsService();
const bookService = new BookService();

async function referenceHigh(bookId: string, symbNo: number) {
  const book = await bookService.get(bookId);

  book.currentSymbol = symbNo;
  await bookService.edit(book);

  router.push("/books/view/" + bookId);
}
</script>

<style scoped>

</style>
