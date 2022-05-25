<template>
  <div class="container-fluid">
    <div class="row flex-nowrap">
      <div class="col-2 px-sm-2 px-0 bg-dark">
        <div
          class="d-flex flex-column align-items-center align-items-sm-start px-3 pt-2 text-white min-vh-100"
        >
          <a
            href="/"
            class="d-flex align-items-center pb-3 mb-md-0 me-md-auto text-white text-decoration-none"
          >
            <span class="fs-5 d-none d-sm-inline">Menu</span>
          </a>
          <ul
            class="nav nav-pills flex-column mb-sm-auto mb-0 align-items-center align-items-sm-start"
            id="menu"
          >
            <li class="nav-item">
              <a href="#" class="nav-link align-middle px-0">
                <i class="fs-4 bi-house"></i>
                <span class="ms-1 d-none d-sm-inline">Home</span>
              </a>
            </li>
            <li>
              <router-link class="nav-link px-0 align-middle" to="test">
                <i class="fs-4 bi-table"></i>
                <span class="ms-1 d-none d-sm-inline">Test</span></router-link
              >
            </li>
            <li>
              <a href="#" class="nav-link px-0 align-middle">
                <i class="fs-4 bi-people"></i>
                <span class="ms-1 d-none d-sm-inline">Customers</span>
              </a>
            </li>
          </ul>
          <hr />
          <div v-if="currentUser" class="dropdown pb-4">
            <a
              href="#"
              class="d-flex align-items-center text-white text-decoration-none dropdown-toggle"
              id="dropdownUser1"
              data-bs-toggle="dropdown"
              aria-expanded="false"
            >
              <img
                src="https://github.com/mdo.png"
                alt="hugenerd"
                width="30"
                height="30"
                class="rounded-circle"
              />
              <span class="d-none d-sm-inline mx-1">loser</span>
            </a>
            <ul
              class="dropdown-menu dropdown-menu-dark text-small shadow"
              aria-labelledby="dropdownUser1"
            >
              <li><a class="dropdown-item" href="#">New project...</a></li>
              <li><a class="dropdown-item" href="#">Settings</a></li>
              <li><a class="dropdown-item" href="#">Profile</a></li>
              <li>
                <hr class="dropdown-divider" />
              </li>
              <li>
                <button class="dropdown-item" @click="logOut">Sign out</button>
              </li>
            </ul>
          </div>
          <ul
            v-else
            class="nav nav-pills flex-column mb-0 align-items-center align-items-sm-start"
            aria-labelledby="dropdownUser1"
          >
            <li>
              <router-link class="nav-link px-0 align-middle" to="login"
                >Login</router-link
              >
            </li>
            <li>
              <router-link class="nav-link px-0 align-middle" to="register"
                >Register</router-link
              >
            </li>
          </ul>
        </div>
      </div>
      <div class="col">
        <router-view />
      </div>
    </div>
  </div>
</template>
<script>
import { ValidationProvider } from "vee-validate";
export default {
  name: "HomeView",
  components: {
    // eslint-disable-next-line vue/no-unused-components
    ValidationProvider,
  },
  computed: {
    currentUser() {
      return this.$store.state.auth.user;
    },
  },
  methods: {
    logOut() {
      if (this.currentUser) {
        this.$store.dispatch("auth/logout").then(
          () => {
            this.$router.push("/home");
          },
          (error) => {
            this.loading = false;
            this.message =
              (error.response && error.response.data.errors.join(", ")) ||
              error.message ||
              error.toString();
          }
        );
      }
    },
  },
};
</script>
<style lang="scss"></style>
