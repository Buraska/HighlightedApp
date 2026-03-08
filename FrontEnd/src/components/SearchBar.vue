<template>
  <div class="space-y-4">
    <div>
      <label for="barr" class="block font-body text-sm font-medium text-ink-800 mb-2">
        Search {{ title }}...
      </label>
      <textarea
        id="barr"
        v-model="input"
        rows="2"
        class="input-field resize-none"
        placeholder="Type to search..."
      ></textarea>
    </div>

    <div class="grid grid-cols-2 gap-4">
      <div class="space-y-2">
        <p class="font-body text-xs font-medium text-ink-800/70 uppercase tracking-wide">Available</p>
        <div class="space-y-2 max-h-48 overflow-y-auto">
          <button
            v-for="item in filteredList(suggestedItems)"
            :key="item.name"
            type="button"
            @click="moveElement(item, suggestedItems, chosenItems)"
            class="w-full text-left px-4 py-2.5 font-body text-sm text-white bg-ink-800/70 hover:bg-ink-800 rounded-xl transition-colors"
          >
            {{ item.name }}
          </button>
          <div>
            <p v-if="input && !filteredList(suggestedItems).length" class="px-4 py-2 font-body text-sm text-red-700 bg-red-50 rounded-xl">
              No results found!
            </p>
            <p v-if="suggestedItems.length === 0 && !input" class="px-4 py-2 font-body text-sm text-ink-800/60 bg-parchment-200 rounded-xl">
              No tags left
            </p>
          </div>
        </div>
      </div>

      <div class="space-y-2">
        <p class="font-body text-xs font-medium text-ink-800/70 uppercase tracking-wide">Selected</p>
        <div class="space-y-2 max-h-48 overflow-y-auto">
          <button
            v-for="item in chosenItems"
            :key="item.name"
            type="button"
            @click="moveElement(item, chosenItems, suggestedItems)"
            class="w-full text-left px-4 py-2.5 font-body text-sm text-white bg-accent-amber hover:bg-accent-gold rounded-xl transition-colors"
          >
            {{ item.name }}
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from "vue";
import type { ISearchBarItem } from "@/components/ISearchBarItem";

const props = defineProps<{
  suggestedItems: ISearchBarItem[];
  chosenItems: ISearchBarItem[];
}>();

const title = "tags";
const input = ref("");

function filteredList(suggestedItems: ISearchBarItem[]) {
  return suggestedItems.filter((item) =>
    item.name.toLowerCase().includes(input.value.toLowerCase())
  );
}

function moveElement(item: ISearchBarItem, fArray: ISearchBarItem[], sArray: ISearchBarItem[]) {
  let i = -1;
  for (const value of fArray) {
    i++;
    if (item.name === value.name) {
      break;
    }
  }

  sArray.push(fArray[i]);
  fArray.splice(i, 1);
}
</script>
