import App from "./App.vue";
import { createApp } from "vue";
import router from "./router";
import store from "./store";
import "bootstrap";
import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap-icons/font/bootstrap-icons.css";
import setupInterceptors from "./services/auth/interceptors";
import Notifications from "@kyvg/vue3-notification";
import VueChartkick from "vue-chartkick";
import "chartkick/chart.js";

createApp(App)
  .use(store)
  .use(router)
  .use(Notifications)
  .use(VueChartkick)
  .mount("#app");
setupInterceptors(store);
