<template>
  <div
    class="card"
    v-if="questionIndex !== quiz.questions.length && isCompleted === false"
  >
    <div class="card-header d-flex justify-content-center">
      <h1>{{ question.name }}</h1>
    </div>
    <div class="card-body d-flex flex-column align-items-center">
      <h5>{{ question.description }}</h5>
      <div class="row w-100 justify-content-center mt-5">
        <div
          class="col-2 d-flex justify-content-center"
          v-for="answer in question.answers"
          :key="answer.id"
        >
          <div class="d-flex">
            <input
              v-model="question.answer"
              class="form-check-input mx-2 fs-4"
              type="radio"
              name="exampleRadios"
              :id="answer.id"
              :value="answer.id"
            />
            <label class="fs-4" :for="answer.id">{{ answer.text }}</label>
          </div>
        </div>
      </div>
    </div>
    <div class="card-footer">
      <div class="w-100">
        <button
          v-if="questionIndex !== 0"
          type="button"
          class="btn btn-primary"
          @click="previousQuestion()"
        >
          <i class="bi bi-arrow-left"></i>Previous
        </button>
        <button
          type="button"
          class="btn btn-primary float-end"
          @click="nextQuestion()"
        >
          Next<i class="bi bi-arrow-right"></i>
        </button>
      </div>
    </div>
  </div>
  <div class="card" v-else-if="isCompleted === false">
    <div class="card-header d-flex justify-content-center">
      <h1>Identification Info</h1>
    </div>
    <div class="card-body d-flex flex-column align-items-center">
      <h5>Please write secret password and your indetificator</h5>
      <div class="row flex-column w-100 align-items-center mt-5">
        <div class="row justify-content-center">
          <div class="fs-6 col-3">Secret password</div>
          <input
            v-model="password"
            type="password"
            class="form-control ml-1 col-9 w-auto"
            placeholder="Password"
          />
        </div>
        <div class="row justify-content-center mt-3">
          <div class="fs-6 col-3">Identificator</div>
          <input
            v-model="identificator"
            type="text"
            class="form-control ml-1 col-9 w-auto"
            placeholder="Identificator"
          />
        </div>
      </div>
    </div>
    <div class="card-footer">
      <div class="w-100">
        <button
          type="button"
          class="btn btn-primary"
          @click="previousQuestion()"
        >
          <i class="bi bi-arrow-left"></i>Previous
        </button>
        <button
          type="button"
          class="btn btn-success float-end"
          @click="submitVote()"
        >
          Submit
        </button>
      </div>
    </div>
  </div>
  <div class="card" v-else>
    <div class="card-body d-flex flex-column align-items-center">
      <div v-if="isSuccessfullyCompleted">
        <h1>Thanks for your vote</h1>
      </div>
      <div v-else>
        <h1>Something wrong. Try again later</h1>
      </div>
    </div>
  </div>
</template>

<script>
import QuizService from "@/services/quiz/quiz.service";

export default {
  name: "QuizView",
  data() {
    return {
      quiz: {
        questions: [],
      },
      question: {
        name: undefined,
        description: undefined,
        answers: [],
        answer: undefined,
      },
      password: undefined,
      identificator: undefined,
      questionIndex: 0,
      isCompleted: false,
      isSuccessfullyCompleted: true,
    };
  },
  async created() {
    this.quiz = (
      await QuizService.getByToken(this.$route.query.token)
    ).data.result;
    this.question = this.quiz.questions[0];
  },
  methods: {
    nextQuestion() {
      this.question = this.quiz.questions[this.questionIndex + 1];
      this.questionIndex += 1;
    },
    previousQuestion() {
      this.question = this.quiz.questions[this.questionIndex - 1];
      this.questionIndex -= 1;
    },
    submitVote() {
      var requestModel = {
        token: this.$route.query.token,
        answers: this.quiz.questions.map((q) => {
          return {
            questionId: q.id,
            answerId: q.answer,
          };
        }),
        password: this.password,
        identificator: this.identificator,
      };
      let self = this;
      QuizService.submitVote(requestModel).then((res) => {
        self.isCompleted = true;
        if (res.data.isSucceeded) {
          self.isSuccessfullyCompleted = true;
        } else {
          self.isSuccessfullyCompleted = false;
        }
      });
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
