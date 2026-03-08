<template>

  <div class="container">
    <div class="row">
      <div class="text-center">
        <h3> Authors</h3>
        <p>Create</p>
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
              <textarea v-model="authorName" style="resize: none" class="form-control" type="text" placeholder="authorName"
                        id="authorName"></textarea>
              <label for="authorName">Author name...</label>
            </div>
          </div>

          <div class="row">
            <div class="m-2 col-8 form-floating">
              <textarea style=" height:150px; resize: none" v-model="description" class="form-control" type="text" placeholder="title"
                        id="description"></textarea>
              <label for="description">Description...</label>
            </div>
          </div>

          <div class="row">
              <div class="m-2 col-1 form-group">
                <input @click="addAuthor()" type="submit" value="Create" class="btn btn-secondary" />
              </div>
          </div>

        </div>
      </div>
    </div>
  </div>

  <div >
    <router-link class="text-danger" to="/authors">Back to List</router-link>
  </div>
</template>

<script setup lang="ts">
import { ref } from "vue";
import { useRouter } from "vue-router";
import { AuthorService } from "@/Services/AuthorService";
import ErrorMessage from "@/components/ErrorMessage.vue";

const router = useRouter();
const authorService = new AuthorService();

const errors = ref<string[]>([]);
const authorName = ref("");
const description = ref("");

async function addAuthor() {
  errors.value = [];

  if (authorName.value.length === 0) {
    errors.value.push("Author name form cannot be empty");
  }

  if (errors.value.length !== 0) {
    return;
  }

  await authorService.add({
    name: authorName.value,
    description: description.value
  });

  router.push("/authors");
}
</script>

<style scoped>

</style>
