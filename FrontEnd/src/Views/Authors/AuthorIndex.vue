<template>

  <div class="container">
    <div class="row">
      <div class="text-center">
        <h3> Authors</h3>
        <p>Read Write Create.</p>
      </div>
    </div>

    <div class="row bg-secondary p-3">
      <div class="col input-group position-relative">
        <input v-model="keywords" type="text" class="form-control" placeholder="Enter Your Keywords" id="keywords">
      </div>

      <div class="col-6"></div>
      <div class="col">
        <router-link to="/authors/add">
          <button style="float: right" type="button" class="btn  btn-block btn-danger btn-custom" id="add-book">
            Add author
          </button>
        </router-link>
      </div>
    </div>

    <div class="filter-result">
      <div v-for="author in filteredList">
        <Author :author="author"></Author>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from "vue";
import { useRouter } from "vue-router";
import { AuthorService } from "@/Services/AuthorService";
import type { IAuthor } from "@/Domain/IAuthor";
import Author from "@/components/Author.vue";

const router = useRouter();
const authorService = new AuthorService();
const authors = ref<IAuthor[]>([]);
const keywords = ref("");

const filteredList = computed(() => {
  return authors.value.filter((item) =>
    item.name.toLowerCase().includes(keywords.value.toLowerCase())
  );
});

onMounted(async () => {
  const res = await authorService.getAll();
  console.log(res);
  if (res === null) {
    router.push("/404");
  } else {
    authors.value = res;
  }
});
</script>

<style scoped>

</style>
