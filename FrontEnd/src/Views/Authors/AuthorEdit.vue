<template>
  <div class="max-w-6xl mx-auto px-4 sm:px-6 lg:px-8 py-12">
    <section class="text-center mb-12">
      <h1 class="font-display text-3xl font-bold text-ink-900 mb-2">Edit Author</h1>
      <p class="font-body text-ink-800/70">Update author details.</p>
    </section>

    <div v-if="errors.length !== 0" class="mb-6 p-4 rounded-xl bg-red-50 border border-red-200 space-y-2">
      <ErrorMessage v-for="error in errors" :key="error" :message="error" />
    </div>

    <div class="bg-white rounded-2xl border border-accent-warm/40 shadow-book p-8 mb-8">
      <div class="space-y-6">
        <div>
          <label for="authorName" class="block font-body text-sm font-medium text-ink-800 mb-2">Author name</label>
          <textarea
            id="authorName"
            v-model="author.name"
            rows="2"
            class="input-field resize-none"
            placeholder="Author name..."
          ></textarea>
        </div>

        <div>
          <label for="description" class="block font-body text-sm font-medium text-ink-800 mb-2">Description</label>
          <textarea
            id="description"
            v-model="author.description"
            rows="6"
            class="input-field resize-none"
            placeholder="Brief description..."
          ></textarea>
        </div>

        <div class="flex gap-4 pt-4">
          <button @click="editAuthor()" type="button" class="btn-secondary">
            Save Changes
          </button>
          <button @click="deleteAuthor()" type="button" class="btn-danger">
            Delete
          </button>
          <router-link to="/authors" class="btn-secondary">
            Cancel
          </router-link>
        </div>
      </div>
    </div>

    <router-link to="/authors" class="font-body text-accent-amber hover:text-accent-gold font-medium transition-colors">
      ← Back to List
    </router-link>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from "vue";
import { useRoute, useRouter } from "vue-router";
import { AuthorService } from "@/Services/AuthorService";
import ErrorMessage from "@/components/ErrorMessage.vue";
import type { IAuthor } from "@/Domain/IAuthor";

const route = useRoute();
const router = useRouter();
const authorService = new AuthorService();

const errors = ref<string[]>([]);
const author = ref<IAuthor>({ name: "", description: "" });

onMounted(async () => {
  const id = route.params["id"].toString();
  author.value = await authorService.get(id);
});

async function editAuthor() {
  errors.value = [];

  if (author.value.name.length === 0) {
    errors.value.push("Author name form cannot be empty");
  }

  if (errors.value.length !== 0) {
    return;
  }

  await authorService.edit(author.value);
  router.push("/authors");
}

async function deleteAuthor() {
  await authorService.delete(author.value.id!);
  router.push("/authors");
}
</script>
