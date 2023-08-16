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
    {{this.book.currentSymbol}}
  </div>
</template>

<script lang="ts">
import { Options, Vue } from "vue-class-component";
import type { IFontFace } from "@/Domain/IFontFace";
import { BookService } from "@/Services/BookService";
import { FontFaceService } from "@/Services/FontFaceService";
import type { IBook } from "@/Domain/IBook";
import { watch } from "vue";

export default class BooksView extends Vue {
  bookService = new BookService();
  fontFaceService = new FontFaceService();

  book:IBook = {
    author: undefined,
    collects: [],
    currentSymbol: 0,
    id: "",
    language: undefined,
    lastAccessAt: "",
    preference: { fontSize:16, fontFace:{name:  "something"} },
    tags: [],
    highlighteds: [], isFinished: false, symbolsTotal: 0, title: "", content: ""}

  text = this.book.content;
  note = "";

  fontFaces: IFontFace[] = [];

  dictWord = { endAt: 0, content: "", startAt: 0 };
  dictMeaning = "";

  async beforeCreate() {
    var id = this.$route.params["id"].toString();
    this.book = await this.bookService.get(id);
    this.fontFaces = await  this.fontFaceService.getAll();

    this.text = this.book.content;

    var l = this.text.split("");
    l.splice(this.book.currentSymbol, 0, "<span id=\"lastPosition\"></span>");
    for (let i = l.length; i > 0; i = i - 3000) {
      var el = document.createElement("span");
      l.splice(i, 0, `<span class="char-anchor" id="${i}"></span>`);
    }
    this.text = l.join("");

    this.highlightQuotes();
    window.addEventListener('scroll', this.changeCurrentSymbol);
  }

  created(){
  }

  beforeMount() {
  }

  mounted() {

  }

  async updated(){
    await this.bookService.edit(this.book)
  }

  scrollToLastPosition() {
    var el = document.getElementById("lastPosition");
        el!.scrollIntoView();
  }


  changeCurrentSymbol() {
    var els = document.getElementsByClassName("char-anchor");

    for (var l of els) {

      var pos = l.getBoundingClientRect();
      if (pos.top >= 0 && pos.bottom <= window.innerHeight) {
        if (this.book.currentSymbol !== parseInt(l.id)){
          this.book.currentSymbol = parseInt(l.id);

          if (this.book.currentSymbol > this.book.currentSymbol - 4000){
            this.book.isFinished = true;

          }
        }
        console.log(this.book.currentSymbol);
      }

    }

  }

  getSelectedText() {

    var text = "";
    var selection = window.getSelection();

    if (selection) {
      if (selection.toString() !== "") {

        text = selection.toString();

        if (this.book.content.indexOf(text) !== -1) {

          if (text.length >= 1000){
            return null;
          }

          var startAt = this.book.content.indexOf(text)

          return { content: text, startAt: startAt, endAt: startAt + text.length };

        }
      }
    }
    return null;
  }

  addNote() {
    if (this.note !== "" || this.note.length < 2000) {
      this.book.highlighteds?.push({
        content: "",
        bookId: this.book.id,
        highlightedType: { name: "note" },
        likes: [],
        orderNo: 0,
        startAt: 0,
        endAt: 0,
        comment: this.note
      });
      this.note = "";
    }
  }

  highlightQuotes() {
    for (var h of this.book.highlighteds) {
      if (h.highlightedType.name === "note") {
        continue;
      }
      console.log("HAAAA");
      var reContent = this.text.match(new RegExp(h.content.split("").join("(?:<.*?>)?")))![0];
      console.log(h);
      console.log(reContent);
      this.text = this.text.replace(reContent, `<span class="${h.highlightedType.name}">` + reContent + "</span>");
    }
  }

  addQuote() {
    var item = this.getSelectedText();

    if (item) {
      this.book.highlighteds?.push({
        bookId: this.book.id,
        highlightedType: { name: "quote" },
        likes: [],
        orderNo: 0,
        startAt: item.startAt,
        endAt: item.endAt,
        content: item.content
      });
    }
    this.highlightQuotes();
  }

  addDict() {
    var item = this.getSelectedText();

    if (item) {
      document.getElementById("dictPopUp")!.style.display = "block";
      this.dictWord = item;

    }
  }

  showPref() {
    var el = document.getElementById("preferences");
    if (el!.style.display === "block") {
      el!.style.display = "none";
    } else {
      el!.style.display = "block";
    }
  }

  hidePopUp() {
    let element = document.getElementById("dictPopUp");
    element!.style.display = "none";
  }

  saveDictWord() {

    this.book.highlighteds?.push({
      bookId: this.book.id,
      highlightedType: { name: "dictionary" },
      likes: [],
      orderNo: 0,
      startAt: this.dictWord.startAt,
      endAt: this.dictWord.endAt,
      content: this.dictWord.content,
      comment: this.dictMeaning
    });
    this.dictMeaning = "";
    this.highlightQuotes();
    this.hidePopUp();
  }


};
</script>

<style>


.quote {
  background-color: #ffdcdc;
}

.dictionary {
  background-color: #b09c7b;
}
</style>