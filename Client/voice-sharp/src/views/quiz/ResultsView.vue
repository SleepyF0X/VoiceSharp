<template>
  <div class="card">
    <div class="card-body d-flex justify-content-center">
      <div
        v-for="data in pieData"
        :key="data[0]"
        class="d-flex flex-column align-items-center"
      >
        <h1>{{ data[0] }}</h1>
        <pie-chart
          :data="
            data[1].reduce((p, c) => {
              var name = c;
              // eslint-disable-next-line no-prototype-builtins
              if (!p.hasOwnProperty(name)) {
                p[name] = 0;
              }
              p[name]++;
              return p;
            }, {})
          "
        ></pie-chart>
      </div>
    </div>
  </div>
</template>

<script>
import QuizService from "@/services/quiz/quiz.service";

export default {
  name: "ResultsView",
  data() {
    return {
      results: undefined,
      pieData: undefined,
      pie: undefined,
    };
  },
  async created() {
    this.results = (
      await QuizService.getResultsByToken(this.$route.query.token)
    ).data.result;
    this.pieData = Object.entries(
      this.groupBy(this.results.map((r) => r.answers).flat(), "question")
    );
    this.pie = this.pieData[0][1].reduce((p, c) => {
      var name = c;
      // eslint-disable-next-line no-prototype-builtins
      if (!p.hasOwnProperty(name)) {
        p[name] = 0;
      }
      p[name]++;
      return p;
    }, {});
  },
  methods: {
    groupBy(objectArray, property) {
      return objectArray.reduce((acc, obj) => {
        const key = obj[property];
        if (!acc[key]) {
          acc[key] = [];
        }
        acc[key].push(obj.answer);
        return acc;
      }, {});
    },
  },
};
</script>

<style scoped>
.card {
  width: 60%;
  min-height: 40%;
}
</style>
