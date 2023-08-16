<template>

  <div v-if="highlighted.highlightedType.name === 'quote'" id="{{highlighted.id}}" style="display: flex; padding: 5px; border: 5px pink inset; align-items: center; margin: 20px" class="row bg-body align-content-center">
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

<script lang="ts">

import { Options, Vue } from "vue-class-component";
import { HighlightedsService } from "@/Services/Highlighteds";
import { BookService } from "@/Services/BookService";

@Options({
  props: {
    highlighted: Object
  }
})
export default class MyHighlighted extends Vue {

  highLightService = new HighlightedsService()
  bookService = new BookService()

  async referenceHigh(bookId: string, symbNo: number){
    var book = await this.bookService.get(bookId);

    book.currentSymbol = symbNo;
    await this.bookService.edit(book);

    this.$router.push("/books/view/" + bookId)
  }


};
</script>

<style scoped>

</style>