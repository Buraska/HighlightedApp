import Homeview from "../Views/Homeview.vue";
import Login from "../Views/identity/Login.vue";
import Register from "../Views/identity/Register.vue";

import { createRouter, createWebHistory } from "vue-router";
import BookIndex from "@/Views/Books/BookIndex.vue";
import BookAdd from "@/Views/Books/BookAdd.vue";
import AuthorIndex from "@/Views/Authors/AuthorIndex.vue";
import AuthorAdd from "@/Views/Authors/AuthorAdd.vue";
import BadRequest from "@/Views/BadRequest.vue";
import BooksView from "@/Views/Books/BooksView.vue";
import BookPreview from "@/Views/Books/BookPreview.vue";
import Highlighteds from "@/Views/Highlighteds/Highlighteds.vue";
import HighlightedsEdit from "@/Views/Highlighteds/HighlightedsEdit.vue";
import { useAppUserStore } from "@/Stores/AppUserStore";
import AuthorEdit from "@/Views/Authors/AuthorEdit.vue";
import { AppUserService } from "@/Services/AppUserService";
import BookEdit from "@/Views/Books/BookEdit.vue";

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {path: "", component: BookIndex},
    {path: "/identity/login", component: Login},
    {path: "/identity/register", component: Register},

    {path: "/books/index", component: BookIndex},
    {path: "/books", component: BookIndex},
    {path: "/books/add", component: BookAdd},
    {path: "/books/view/:id", component: BooksView},
    {path: "/books/preview:id", component: BookPreview},
    {path: "/books/edit/:id", component: BookEdit},
    {path: "/authors", component: AuthorIndex},
    {path: "/authors/add", component: AuthorAdd},
    {path: "/authors/edit/:id", component: AuthorEdit},
    {path: "/highlighted/edit/:id", component: HighlightedsEdit},
    {path: "/highlighted/:bookId", component: Highlighteds},

    {path: "/:pathMatch(.*)*", component: BadRequest},
  ]
});

router.beforeEach(async (to) => {
  // redirect to login page if not logged in and trying to access a restricted page
  const publicPages = ['/identity/login', "/identity/register", "/"];
  const authRequired = !publicPages.includes(to.path);
  const auth = useAppUserStore();

  if (authRequired && !auth.jwt) {
    return '/identity/login';
  }

});
export default router;
