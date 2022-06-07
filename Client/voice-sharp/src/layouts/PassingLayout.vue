<template>
  <div class="content d-flex align-items-center justify-content-center">
    <slot />
  </div>
</template>

<script>
export default {
  name: "PassingLayout",
};
</script>

<style scoped>
.content {
  height: 100%;
  width: 100%;
  background-image: url("~@/assets/images/bg.jpg");
}
</style>

<style>
body {
  height: 100vh;
}
#app {
  height: 100%;
}
</style>
