<template>

  <div  :id="book.id" style="display: flex; padding: 5px; border: 5px pink inset; align-items: center; margin: 20px" class="row bg-body align-content-center">

    <div style="" class="col-2 text-break">
      <h6 class="">{{ book.title }}</h6>
    </div>

    <div style="border-left-style: solid;  border-color: darkgray" class="col-4 text-break">
      Author:
      <div v-if="book.author">
        <h6>{{ book.author.name }}</h6>
      </div>
    </div>

    <div  style="border-left-style: solid; border-right-style: solid; border-color: darkgray" class="col-2">
      process: {{ Math.round((book.currentSymbol / book.symbolsTotal) * 100) }}%
    </div>

    <div  style="border-right-style: solid; border-color: darkgray" class="col-2">
      finished: {{ book.isFinished }}
    </div>

    <div class="col-2">
      <div class="dropdown">
        <button style="" class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton1" data-bs-toggle="dropdown" aria-expanded="false">
          action
        </button>
        <ul class="dropdown-menu" aria-labelledby="dropdownMenuButton1">
          <li class="bg-danger "><router-link :to="'/books/preview' + book.id" class="text-white dropdown-item">READ</router-link></li>
          <li><router-link :to="'/highlighted/' + book.id " class=" dropdown-item">highlighted</router-link></li>
          <li><router-link class="dropdown-item" :to="'/books/edit/'+book.id">edit</router-link>  </li>
        </ul>
      </div>
    </div>

  </div>
</template>

<script setup lang="ts">
import type { IBook } from "@/Domain/IBook";

defineProps<{
  book: IBook;
}>();
</script>

<style scoped>

</style>