<template>
  <div>
    <input v-model="message" />
  </div>
</template>

<script>
import TestService from "@/services/test.service";

export default {
  name: "TestView",
  data() {
    return {
      message: "",
    };
  },
  async mounted() {
    this.message = await TestService.getTestData();
  },
};
</script>

<style scoped></style>
