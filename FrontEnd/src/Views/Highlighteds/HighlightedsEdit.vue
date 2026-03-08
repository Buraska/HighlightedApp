<template>
  <div class="max-w-6xl mx-auto px-4 sm:px-6 lg:px-8 py-12">
    <section class="text-center mb-12">
      <h1 class="font-display text-3xl font-bold text-ink-900 mb-2">Edit Highlight</h1>
      <p class="font-body text-ink-800/70">Update your highlight.</p>
    </section>

    <div v-if="errors.length !== 0" class="mb-6 p-4 rounded-xl bg-red-50 border border-red-200 space-y-2">
      <ErrorMessage v-for="error in errors" :key="error" :message="error" />
    </div>

    <div class="bg-white rounded-2xl border border-accent-warm/40 shadow-book p-8 mb-8">
      <div class="space-y-6">
        <div v-if="highlighted.highlightedType.name !== 'quote'">
          <label for="description" class="block font-body text-sm font-medium text-ink-800 mb-2">Content</label>
          <textarea
            id="description"
            v-model="highlighted.comment"
            rows="6"
            class="input-field resize-none"
            placeholder="Edit content..."
          ></textarea>
        </div>

        <div class="flex gap-4 pt-4">
          <button v-if="highlighted.highlightedType.name !== 'quote'" @click="editHighlighted()" type="button" class="btn-secondary">
            Save Changes
          </button>
          <button @click="deleteHighlighted()" type="button" class="btn-danger">
            Delete
          </button>
          <router-link :to="'/highlighted/' + highlighted.bookId" class="btn-secondary">
            Cancel
          </router-link>
        </div>
      </div>
    </div>

    <router-link :to="'/highlighted/' + highlighted.bookId" class="font-body text-accent-amber hover:text-accent-gold font-medium transition-colors">
      ← Back to List
    </router-link>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from "vue";
import { useRoute, useRouter } from "vue-router";
import { HighlightedsService } from "@/Services/Highlighteds";
import ErrorMessage from "@/components/ErrorMessage.vue";
import type { IHighlighted } from "@/Domain/Highlighted";

const route = useRoute();
const router = useRouter();
const highlightedsService = new HighlightedsService();

const errors = ref<string[]>([]);
const highlighted = ref<IHighlighted>({
  endAt: 0,
  highlightedType: { name: "none" },
  likes: [],
  orderNo: 0,
  startAt: 0,
  content: "",
  comment: "",
  bookId: ""
});

onMounted(async () => {
  const id = route.params["id"].toString();
  highlighted.value = await highlightedsService.get(id);
});

async function editHighlighted() {
  errors.value = [];

  if (highlighted.value.comment?.length === 0 || (highlighted.value.comment?.length ?? 0) > 2000) {
    errors.value.push("Comment has to be in range of 1 and 2000 characters");
  }

  if (errors.value.length !== 0) {
    return;
  }

  await highlightedsService.edit(highlighted.value);
  router.push("/highlighted/" + highlighted.value.bookId);
}

async function deleteHighlighted() {
  await highlightedsService.delete(highlighted.value.id!);
  router.push("/highlighted/" + highlighted.value.bookId);
}
</script>
