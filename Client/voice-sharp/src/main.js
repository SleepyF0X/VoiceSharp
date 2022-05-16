import App from "./App.vue";
import { createApp } from "vue";
import router from "./router";
import store from "./store";
import "bootstrap";
import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap-icons/font/bootstrap-icons.css";
import setupInterceptors from "./services/auth/interceptors";

createApp(App).use(store).use(router).mount("#app");
setupInterceptors(store);
