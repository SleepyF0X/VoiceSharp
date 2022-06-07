<template>
  <div class="container d-flex align-items-center justify-content-center">
    <slot />
  </div>
</template>

<script>
export default {
  name: "PassingLayout",
};
</script>

<style>
body {
  background-image: url("~@/assets/images/bg.jpg");
  height: 100vh;
}
.container {
  height: 100%;
  width: 100%;
}
#app {
  height: 100%;
}
</style>
