<template>

  <div class="container">
    <div class="row">
      <div class="text-center">
        <h3> Authors</h3>
        <p>Edit</p>
      </div>
    </div>
  </div>

  <hr />

  <div style="">

    <div class="row">
      <div class="col">
        <div v-if="errors.length !== 0" class="text-danger validation-summary-errors" data-valmsg-summary="true">
          <div v-for="error in errors">
            <ErrorMessage :message="error"></ErrorMessage>
          </div>
        </div>

        <div class="container" style="border-bottom: 5px pink inset; align-items: center;">

          <div class="row">
            <div class="m-2  col-4 form-floating">
              <textarea v-model="author.name" style="resize: none" class="form-control" type="text"
                        placeholder="authorName"
                        id="authorName"></textarea>
              <label for="authorName">Author name...</label>
            </div>
          </div>

          <div class="row">
            <div class="m-2 col-8 form-floating">
              <textarea style=" height:150px; resize: none" v-model="author.description" class="form-control"
                        type="text" placeholder="title"
                        id="description"></textarea>
              <label for="description">Description...</label>
            </div>
          </div>

          <div class="row">
            <div class="m-2 col-1 form-group">
              <input @click="editAuthor()" type="submit" value="Edit" class="btn btn-secondary" />
            </div>

            <div class="m-2 col-1 form-group">
              <input @click="deleteAuthor()" type="submit" value="Delete" class="btn btn-danger" />
            </div>
          </div>

        </div>
      </div>
    </div>
  </div>

  <div>
    <router-link class="text-danger" to="/authors">Back to List</router-link>
  </div>
</template>

<script lang="ts">

import { Options, Vue } from "vue-class-component";
import { AuthorService } from "@/Services/AuthorService";
import ErrorMessage from "@/components/ErrorMessage.vue";
import type { IAuthor } from "@/Domain/IAuthor";

@Options({
  components: { ErrorMessage }
})

export default class AuthorEdit extends Vue {

  authorService = new AuthorService();

  errors: string[] = [];
  author: IAuthor = { name: "", description: "" };

  async beforeCreate() {
    var id = this.$route.params["id"].toString();
    this.author = await this.authorService.get(id);
  }

  async editAuthor() {
    this.errors = [];

    if (this.author.name.length === 0) {
      this.errors.push("Author name form cannot be empty");
    }

    if (this.errors.length !== 0) {
      return;
    }

    await this.authorService.edit(this.author);
    this.$router.push("/authors");
  }

  async deleteAuthor(){
    await   this.authorService.delete(this.author.id!);
    this.$router.push("/authors");

  }

};
</script>

<style scoped>

</style>