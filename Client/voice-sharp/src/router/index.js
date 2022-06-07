import { createRouter, createWebHistory } from "vue-router";
import LoginView from "@/views/auth/LoginView";
import store from "../store/index";
import EditorView from "@/views/quiz/EditorView";
import ListView from "@/views/quiz/ListView";
import QuizView from "@/views/quiz/QuizView";
import ResultsView from "@/views/quiz/ResultsView";

const routes = [
  {
    path: "/",
    name: "home",
    meta: { layout: "DefaultLayout" },
    component: ListView,
  },
  {
    path: "/login",
    name: "login",
    meta: { layout: "PassingLayout" },
    component: LoginView,
  },
  {
    path: "/quiz/create",
    name: "quizCreator",
    meta: { layout: "DefaultLayout" },
    component: EditorView,
  },
  {
    path: "/quiz/edit/:id",
    name: "quizEditor",
    meta: { layout: "DefaultLayout" },
    component: EditorView,
  },
  {
    path: "/quizzes",
    name: "quizzes",
    meta: { layout: "DefaultLayout" },
    component: ListView,
  },
  {
    path: "/quizzes/passing",
    name: "quiz",
    meta: { layout: "PassingLayout" },
    component: QuizView,
  },
  {
    path: "/quizzes/results",
    name: "quizResults",
    meta: { layout: "PassingLayout" },
    component: ResultsView,
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

router.beforeEach(async (to, from, next) => {
  const publicPages = [
    "/login",
    "/register",
    "/home",
    "/quizzes/passing",
    "/quizzes/results",
  ];
  const authRequired = !publicPages.includes(to.path);
  const loggedIn = await store.state.auth.user;
  // trying to access a restricted page + not logged in
  // redirect to login page
  if (authRequired && !loggedIn) {
    next("/login");
  } else {
    next();
  }
});

export default router;
