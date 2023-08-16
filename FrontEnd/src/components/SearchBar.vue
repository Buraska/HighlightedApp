<template>
  <div class="container">


    <div class="row">
      <div class="col-4  form-floating">
              <textarea style="resize: none" v-model="input" class="form-control" type="text" placeholder="title"
                        id="barr"></textarea>

        <label for="barr">Search {{ title }} ...</label>
      </div>

    </div>

    <div class="row">

      <div class="col-3 m-1">
        <div v-on:click="moveElement(item, suggestedItems, chosenItems)" class="item"
             v-for="item in filteredList(suggestedItems)">
          {{ item.name }}
        </div>

        <div class="item error" v-if="input&&!filteredList(suggestedItems).length">
          <p>No results found!</p>
        </div>

        <div class="item missing" v-if="suggestedItems.length === 0">
          <p>No tags left</p>
        </div>
      </div>

      <div class="col-3 m-1">
        <div class="item chosen" v-for="item in chosenItems">
          <p v-on:click="moveElement(item, chosenItems, suggestedItems)">{{ item.name }}</p>
        </div>
      </div>

    </div>

  </div>
</template>

<script lang="ts">
import { Options, Vue } from "vue-class-component";
import type { ISearchBarItem } from "@/components/ISearchBarItem";

@Options({
  props: {
    suggestedItems: Array,
    chosenItems: Array
  }
})

export default class SearchBar extends Vue {
  title = "tags";
  input = "";

  filteredList(suggestedItems: ISearchBarItem[]) {
    return suggestedItems.filter((item) =>
      item.name.toLowerCase().includes(this.input.toLowerCase())
    );
  }

  moveElement(item: ISearchBarItem, fArray: ISearchBarItem[], sArray: ISearchBarItem[]) {
    var i = -1;
    for (let value of fArray) {
      i++;
      if (item.name === value.name) {
        break;
      }
    }

    sArray.push(fArray[i]);
    fArray.splice(i, 1);
  }
}
</script>

<style scoped>

* {
  padding: 0;
  margin: 0;
  box-sizing: border-box;
}

.col-auto {
  margin: 3px;
}

body {
  padding: 20px;
  min-height: 100vh;
  background-color: rgb(234, 242, 255);
}

input {
  display: block;
  width: 350px;
  margin: 20px auto;
  padding: 10px 45px;
  background-size: 15px 15px;
  font-size: 16px;
  border: none;
  border-radius: 5px;
  box-shadow: rgba(50, 50, 93, 0.25) 0px 2px 5px -1px,
  rgba(0, 0, 0, 0.3) 0px 1px 3px -1px;
}

.item {
  margin: 0 auto 10px auto;
  padding: 10px 20px;
  color: white;
  border-radius: 5px;
  background-color: rgb(152, 120, 120);
  cursor: pointer;
}

.error {
  background-color: tomato;
}

.missing {
  background-color: rgb(164, 159, 159);
  color: #000000;

}

.chosen{
  background-color: rgb(147, 54, 54);
  color: #ffffff;


}
</style>