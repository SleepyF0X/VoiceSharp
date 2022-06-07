<template>
  <div>sadf</div>
</template>

<script>
import QuizService from "@/services/quiz/quiz.service";

export default {
  name: "ResultsView",
  data() {
    return {
      results: undefined,
    };
  },
  async created() {
    this.result = (
      await QuizService.getResultsByToken(this.$route.query.token)
    ).data.result;
    this.question = this.quiz.questions[0];
  },
};
</script>

<style scoped></style>
