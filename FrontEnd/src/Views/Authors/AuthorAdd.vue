<template>
  <div class="max-w-6xl mx-auto px-4 sm:px-6 lg:px-8 py-12">
    <section class="text-center mb-12">
      <h1 class="font-display text-3xl font-bold text-ink-900 mb-2">Add Author</h1>
      <p class="font-body text-ink-800/70">Create a new author profile.</p>
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
            v-model="authorName"
            rows="2"
            class="input-field resize-none"
            placeholder="Author name..."
          ></textarea>
        </div>

        <div>
          <label for="description" class="block font-body text-sm font-medium text-ink-800 mb-2">Description</label>
          <textarea
            id="description"
            v-model="description"
            rows="6"
            class="input-field resize-none"
            placeholder="Brief description..."
          ></textarea>
        </div>

        <div class="flex gap-4 pt-4">
          <button @click="addAuthor()" type="button" class="btn-secondary">
            Create Author
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
