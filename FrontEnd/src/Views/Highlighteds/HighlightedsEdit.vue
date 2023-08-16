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

<script lang="ts">

import { Options, Vue } from "vue-class-component";
import { AuthorService } from "@/Services/AuthorService";
import ErrorMessage from "@/components/ErrorMessage.vue";
import type { IAuthor } from "@/Domain/IAuthor";
import type { IHighlighted } from "@/Domain/Highlighted";
import { HighlightedsService } from "@/Services/Highlighteds";

@Options({
  components: { ErrorMessage }
})

export default class HighlightedsEdit extends Vue {

  highlightedsService = new HighlightedsService();

  errors: string[] = [];
  highlighted: IHighlighted = {
    endAt: 0,
    highlightedType: { name:"none"},
    likes: [],
    orderNo: 0,
    startAt: 0,
    content: "", comment: "" };

  async beforeCreate() {
    var id = this.$route.params["id"].toString();
    this.highlighted = await this.highlightedsService.get(id);
  }

  async editHighlighted() {
    this.errors = [];

    if (this.highlighted.comment?.length === 0 || this.highlighted.comment!.length > 2000) {
      this.errors.push("Comment has to be in range of 1 and 2000 characters");
    }

    if (this.errors.length !== 0) {
      return;
    }

    await this.highlightedsService.edit(this.highlighted);
    this.$router.push("/highlighted/" + this.highlighted.bookId);
  }

  async deleteHighlighted(){
    await   this.highlightedsService.delete(this.highlighted.id!);
    this.$router.push("/highlighted/" + this.highlighted.bookId);

  }

};
</script>

<style scoped>

</style>