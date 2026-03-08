<template>

  <div class="container">
    <div class="row">
      <div class="text-center">
        <h3>Highlighted</h3>
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
            <div v-if="highlighted.highlightedType.name !== 'quote'" class="m-2 col-8 form-floating">
              <textarea style=" height:150px; resize: none" v-model="highlighted.comment" class="form-control"
                        type="text" placeholder="title"
                        id="description"></textarea>
              <label for="description">Content</label>
            </div>
          </div>

          <div class="row">
            <div v-if="highlighted.highlightedType.name !== 'quote'" class="m-2 col-1 form-group">
              <input @click="editHighlighted()" type="submit" value="Edit" class="btn btn-secondary" />
            </div>

            <div class="m-2 col-1 form-group">
              <input @click="deleteHighlighted()" type="submit" value="Delete" class="btn btn-danger" />
            </div>
          </div>

        </div>
      </div>
    </div>
  </div>

  <div>
    <router-link class="text-danger" :to="'/highlighted/' + highlighted.bookId">Back to List</router-link>
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

<style scoped>

</style>
