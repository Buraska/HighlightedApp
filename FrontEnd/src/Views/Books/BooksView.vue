<template>
  <div class="max-w-6xl mx-auto px-4 sm:px-6 lg:px-8 py-6">
    <!-- Dictionary Popup -->
    <div
      id="dictPopUp"
      class="fixed z-50 right-[10vw] w-[300px] bg-white rounded-2xl shadow-book-hover border border-accent-warm/40 p-4"
      :class="{ hidden: !dictPopUpVisible }"
      style="top: 120px"
    >
      <button
        v-on:click="hidePopUp"
        class="absolute top-2 right-2 w-7 h-7 flex items-center justify-center rounded-lg hover:bg-parchment-100 text-ink-800"
        aria-label="Close"
      >
        &#10006;
      </button>
      <div class="font-body text-ink-800 mb-2">Word: {{ dictWord.content }}</div>
      <textarea
        v-model="dictMeaning"
        class="input-field resize-none mb-4"
        rows="3"
        placeholder="Meaning..."
      ></textarea>
      <button v-on:click="saveDictWord" class="btn-primary w-full">
        Save
      </button>
    </div>

    <div class="flex flex-col lg:flex-row gap-6">
      <!-- Left sidebar -->
      <div class="lg:w-48 shrink-0 order-2 lg:order-1">
        <div class="lg:sticky lg:top-24 space-y-4">
          <router-link :to="'/books/preview' + book.id" class="btn-secondary w-full block text-center">
            To preview
          </router-link>
          <div class="relative">
            <button
              v-on:click="showPref"
              class="p-2 rounded-lg hover:bg-parchment-100 text-ink-800 opacity-70 hover:opacity-100 transition-opacity"
              aria-label="Preferences"
            >
              &#9881;
            </button>
            <div
              v-show="prefVisible"
              id="preferences"
              class="absolute left-0 top-full mt-2 p-4 bg-white rounded-xl shadow-book border border-accent-warm/40 min-w-[200px]"
            >
              <div class="space-y-4">
                <div>
                  <label for="fontFace" class="block font-body text-sm font-medium text-ink-800 mb-1">Font</label>
                  <select v-model="book.preference.fontFace" id="fontFace" class="input-field text-sm">
                    <option v-for="face in fontFaces" :key="face.name" :value="face">{{ face.name }}</option>
                  </select>
                </div>
                <div>
                  <label for="fontSize" class="block font-body text-sm font-medium text-ink-800 mb-1">
                    Size: {{ book.preference.fontSize }}px
                  </label>
                  <input
                    v-model="book.preference.fontSize"
                    type="range"
                    min="10"
                    max="26"
                    id="fontSize"
                    class="w-full accent-accent-amber"
                  />
                </div>
                <button v-on:click="scrollToLastPosition" class="btn-secondary w-full text-sm">
                  To last position
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>

      <!-- Main content -->
      <div
        id="text-placeholder"
        class="flex-1 min-w-0 bg-parchment-50 rounded-2xl border border-accent-warm/40 p-8 font-body text-ink-800 leading-relaxed whitespace-pre-line"
        :style="{ fontSize: book.preference.fontSize + 'px', fontFamily: book.preference.fontFace.name }"
        v-html="text"
      ></div>

      <!-- Right sidebar -->
      <div class="lg:w-48 shrink-0 order-1 lg:order-3 space-y-4">
        <div class="lg:sticky lg:top-24 space-y-4">
          <button v-on:click="addQuote" class="btn-secondary w-full">
            Quote
          </button>
          <button v-on:click="addDict" class="btn-secondary w-full">
            Dictionary
          </button>
          <div>
            <textarea
              v-model="note"
              class="input-field resize-none mb-2"
              rows="6"
              placeholder="Note..."
            ></textarea>
            <button v-on:click="addNote" class="btn-primary w-full">
              Write down
            </button>
          </div>
        </div>
      </div>
    </div>

    <div class="hidden">{{ book.currentSymbol }}</div>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, onMounted, onUpdated, onBeforeUnmount } from "vue";
import { useRoute } from "vue-router";
import type { IFontFace } from "@/Domain/IFontFace";
import { BookService } from "@/Services/BookService";
import { FontFaceService } from "@/Services/FontFaceService";
import type { IBook } from "@/Domain/IBook";

const route = useRoute();
const bookService = new BookService();
const fontFaceService = new FontFaceService();

const book = reactive<IBook>({
  author: undefined,
  collects: [],
  currentSymbol: 0,
  id: "",
  language: undefined,
  lastAccessAt: "",
  preference: { fontSize: 16, fontFace: { name: "something" } },
  tags: [],
  highlighteds: [],
  isFinished: false,
  symbolsTotal: 0,
  title: "",
  content: ""
});

const text = ref("");
const note = ref("");
const fontFaces = ref<IFontFace[]>([]);
const dictWord = ref({ endAt: 0, content: "", startAt: 0 });
const dictMeaning = ref("");
const dictPopUpVisible = ref(false);
const prefVisible = ref(false);

function scrollToLastPosition() {
  const el = document.getElementById("lastPosition");
  el!.scrollIntoView();
}

function changeCurrentSymbol() {
  const els = document.getElementsByClassName("char-anchor");

  for (const l of els) {
    const pos = (l as Element).getBoundingClientRect();
    if (pos.top >= 0 && pos.bottom <= window.innerHeight) {
      const idNum = parseInt((l as HTMLElement).id, 10);
      if (book.currentSymbol !== idNum) {
        book.currentSymbol = idNum;

        if (book.currentSymbol > book.currentSymbol - 4000) {
          book.isFinished = true;
        }
      }
    }
  }
}

function getSelectedText(): { content: string; startAt: number; endAt: number } | null {
  const selection = window.getSelection();

  if (selection && selection.toString() !== "") {
    const textVal = selection.toString();

    if (book.content.indexOf(textVal) !== -1) {
      if (textVal.length >= 1000) {
        return null;
      }

      const startAt = book.content.indexOf(textVal);
      return { content: textVal, startAt, endAt: startAt + textVal.length };
    }
  }
  return null;
}

function addNote() {
  if (note.value !== "" && note.value.length < 2000) {
    book.highlighteds?.push({
      content: "",
      bookId: book.id!,
      highlightedType: { name: "note" },
      likes: [],
      orderNo: 0,
      startAt: 0,
      endAt: 0,
      comment: note.value
    });
    note.value = "";
  }
}

function highlightQuotes() {
  for (const h of book.highlighteds) {
    if (h.highlightedType.name === "note") {
      continue;
    }
    const reContent = text.value.match(new RegExp(h.content.split("").join("(?:<.*?>)?")));
    if (reContent) {
      text.value = text.value.replace(reContent[0], `<span class="${h.highlightedType.name}">` + reContent[0] + "</span>");
    }
  }
}

function addQuote() {
  const item = getSelectedText();

  if (item) {
    book.highlighteds?.push({
      bookId: book.id!,
      highlightedType: { name: "quote" },
      likes: [],
      orderNo: 0,
      startAt: item.startAt,
      endAt: item.endAt,
      content: item.content
    });
    highlightQuotes();
  }
}

function addDict() {
  const item = getSelectedText();

  if (item) {
    dictWord.value = item;
    dictPopUpVisible.value = true;
  }
}

function showPref() {
  prefVisible.value = !prefVisible.value;
}

function hidePopUp() {
  dictPopUpVisible.value = false;
}

function saveDictWord() {
  book.highlighteds?.push({
    bookId: book.id!,
    highlightedType: { name: "dictionary" },
    likes: [],
    orderNo: 0,
    startAt: dictWord.value.startAt,
    endAt: dictWord.value.endAt,
    content: dictWord.value.content,
    comment: dictMeaning.value
  });
  dictMeaning.value = "";
  highlightQuotes();
  hidePopUp();
}

onMounted(async () => {
  const id = route.params["id"].toString();
  Object.assign(book, await bookService.get(id));
  if (!book.preference) {
    book.preference = { fontSize: 16, fontFace: { name: "something" } };
  }
  fontFaces.value = await fontFaceService.getAll();

  text.value = book.content;

  const l = text.value.split("");
  l.splice(book.currentSymbol, 0, "<span id=\"lastPosition\"></span>");
  for (let i = l.length; i > 0; i = i - 3000) {
    l.splice(i, 0, `<span class="char-anchor" id="${i}"></span>`);
  }
  text.value = l.join("");

  highlightQuotes();
  window.addEventListener("scroll", changeCurrentSymbol);
});

onBeforeUnmount(() => {
  window.removeEventListener("scroll", changeCurrentSymbol);
});

onUpdated(async () => {
  await bookService.edit(book);
});
</script>

<style scoped>
.quote {
  background-color: #ffdcdc;
}

.dictionary {
  background-color: #b09c7b;
}
</style>
