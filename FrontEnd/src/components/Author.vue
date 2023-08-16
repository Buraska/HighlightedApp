<template>

  <div  id="{{author.id}}" style="display: flex; padding: 5px; border: 5px pink inset; align-items: center; margin: 20px" class="row bg-body align-content-center">
    <div style="" class="col-2 text-break">
      <h6>{{ author.name }}</h6>
    </div>

    <div  style="border-left-style: solid;border-right-style: solid; border-color: darkgray" class="col-8 text-break">
      {{ author.description }}
    </div>

    <div class="col-2">
      <div class="dropdown">
        <button style="" class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton1" data-bs-toggle="dropdown" aria-expanded="false">
          action
        </button>
        <ul class="dropdown-menu" aria-labelledby="dropdownMenuButton1">
<!--          <li><a class="dropdown-item" href="#">books</a></li>-->
<!--          <li><a class="dropdown-item" href="#">highlighted</a></li>-->
          <li><router-link class="dropdown-item" :to="'/authors/edit/'+author.id">edit</router-link>  </li>
        </ul>
      </div>
    </div>

  </div>
</template>

<script lang="ts">

import { Options, Vue } from "vue-class-component";
import type { IAuthor } from "../Domain/IAuthor";

@Options({
  props: {
    author: Object
  }
})
export default class Author extends Vue {


};
</script>

<style scoped>

</style>