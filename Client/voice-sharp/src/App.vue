<template>
  <component :is="layout">
    <notifications position="top center" />
    <router-view />
  </component>
</template>
<script>
import { ValidationProvider } from "vee-validate";
import DefaultLayout from "@/layouts/DefaultLayout";
import PassingLayout from "@/layouts/PassingLayout";
export default {
  name: "HomeView",
  components: {
    // eslint-disable-next-line vue/no-unused-components
    ValidationProvider,
    DefaultLayout,
    PassingLayout,
  },
  computed: {
    layout() {
      console.log(this.$route.meta.layout);
      return this.$route.meta.layout || "";
    },
  },
};
</script>
<style lang="scss"></style>
