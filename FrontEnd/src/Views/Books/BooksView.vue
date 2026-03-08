<template>
  <div class="container">


    <div id="dictPopUp"
         style="display:none; right:40vw;width: 300px; position: fixed; background-color:rgb(169,156,156)"
         class="container">

      <div v-on:click="hidePopUp" style="cursor: pointer; width: 7px" class="float-end m-2">&#10006;</div>

      <div style="margin: 10px"
           class="row">
        word: {{ dictWord.content }}
      </div>
      <div style="margin: 2px;" class="row">
          <textarea style="; resize:none;" v-model="dictMeaning"
                    class="form-control" type="text"
                    placeholder="means..."></textarea>
      </div>
      <div style="margin: 2px; margin-bottom: 10px;margin-top: 10px;" class="row">
        <button v-on:click="saveDictWord" style="width: 70px" class="btn-dark">
          save
        </button>
      </div>
    </div>


    <div class="row">
      <div class="col-2">
        <div style="position: fixed; left: 10px; width: 200px">
          <div><router-link :to="'/books/preview' + book.id"><button class="btn-dark">to preview</button></router-link></div>
          <div v-on:click="showPref" style="; cursor: pointer; width: 20px; opacity: 0.7">&#9881</div>
          <div style="display: none;padding:10px; background-color: rgb(169,156,156)" id="preferences">
            <div class="form-group">
              <label for="fontFace">font face</label>
              <select v-model="book.preference.fontFace" class="form-control" id="fontFace">
                <option :value="face" v-for="face in fontFaces">{{ face.name }}</option>
              </select>
            </div>
            <div>
              <label for="fontSize">Font size</label>
              <input v-model="book.preference.fontSize" type="range" class="custom-range" min="10" max="26"
                     id="fontSize">
            </div>
            <div style="margin-top: 5px">
              <button v-on:click="scrollToLastPosition" style="" class="btn-dark">to last position</button>
            </div>
          </div>
        </div>
      </div>
      <div id="text-placeholder" class="col-8 bg-light" style="white-space: pre-line"
           :style="{fontSize: book.preference.fontSize + 'px', fontFamily: book.preference.fontFace.name }"
           v-html="text"></div>

      <div class="col-2" style="position:fixed;top:100px;float:right;right:30px;">
        <div style="width: 100px;height: 35px; margin-bottom: 20px" class="button row">
          <button v-on:click="addQuote" class="btn-dark">
            quote
          </button>
        </div>
        <div style="width: 100px;height: 35px; margin-bottom: 20px" class="row">
          <button v-on:click="addDict" class="btn-dark">
            dictionary
          </button>
        </div>
        <div class="row">
          <textarea style="resize:none; margin-bottom: 10px; width: 200px; height: 200px" v-model="note"
                    class="form-control" type="text"
                    placeholder="Note..." id="title"></textarea>
          <button v-on:click="addNote" style="height: 35px; width: 120px" class="btn-dark">
            write down
          </button>
        </div>
      </div>
    </div>
  </div>
  <div style="display: none">
    {{ book.currentSymbol }}
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
    document.getElementById("dictPopUp")!.style.display = "block";
    dictWord.value = item;
  }
}

function showPref() {
  const el = document.getElementById("preferences");
  if (el!.style.display === "block") {
    el!.style.display = "none";
  } else {
    el!.style.display = "block";
  }
}

function hidePopUp() {
  const element = document.getElementById("dictPopUp");
  element!.style.display = "none";
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

<style>


.quote {
  background-color: #ffdcdc;
}

.dictionary {
  background-color: #b09c7b;
}
</style>
