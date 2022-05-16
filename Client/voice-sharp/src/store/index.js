import { createStore } from "vuex";
import { auth } from "@/store/auth/auth.module";
export default createStore({
  modules: {
    auth,
  },
});
