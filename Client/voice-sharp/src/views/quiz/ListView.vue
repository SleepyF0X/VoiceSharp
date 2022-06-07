<template>
  <div class="mt-3">
    <router-link class="btn btn-primary w-25" to="/quiz/create">
      <i class="fs-4 bi-file-text"></i>
      <span class="ms-1 d-none d-sm-inline"> Create Quiz</span></router-link
    >
    <div class="card mt-3" v-for="quiz in quizzes" :key="quiz.id">
      <div class="card-body">
        <div class="row">
          <div class="col-7">
            <h2>{{ quiz.name }}</h2>
            {{ quiz.description }}
          </div>
          <div class="col-5 d-flex align-items-center justify-content-end">
            <button
              type="button"
              data-bs-toggle="modal"
              data-bs-target="#exampleModal"
              class="btn btn-success w-25"
              @click="getQuizVoters(quiz.id)"
            >
              <i class="bi-people"></i>
              Add voters
            </button>
            <router-link
              class="btn btn-warning w-25 ms-3"
              :to="{ name: 'quizEditor', params: { id: quiz.id } }"
              >Edit</router-link
            >
            <button
              class="btn btn-danger w-25 ms-3"
              @click="deleteQuiz(quiz.id)"
            >
              Delete
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
  <div
    class="modal fade"
    id="exampleModal"
    tabindex="-1"
    aria-labelledby="exampleModalLabel"
    aria-hidden="true"
  >
    <div class="modal-dialog modal-lg">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="exampleModalLabel">Voters</h5>
          <button
            type="button"
            class="btn-close"
            data-bs-dismiss="modal"
            aria-label="Close"
          ></button>
        </div>
        <div class="modal-body">
          <textarea
            class="form-control"
            placeholder="Enter emails"
            style="height: 80px"
            v-model="emails"
          />
        </div>
        <div class="modal-footer">
          <button
            type="button"
            class="btn btn-secondary"
            data-bs-dismiss="modal"
          >
            Close
          </button>
          <button type="button" class="btn btn-success" @click="addVoters()">
            Add voters
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import QuizService from "../../services/quiz/quiz.service";
export default {
  name: "ListView",
  data() {
    return {
      quizId: undefined,
      emails: undefined,
      quizzes: [],
    };
  },
  mounted() {
    this.getData();
  },
  methods: {
    async getData() {
      this.quizzes = (await QuizService.getAll()).data.result;
    },
    deleteQuiz(quizId) {
      let self = this;
      QuizService.delete(quizId).then((res) => {
        if (res.data.isSucceeded) {
          self.quizzes = self.quizzes.filter((x) => {
            return x.id !== quizId;
          });
          this.$notify({
            title: "Successfully deleted",
            type: "success",
          });
        } else {
          this.$notify({
            title: "Delete fail",
            type: "danger",
          });
        }
      });
    },
    addVoters() {
      QuizService.addVotersToQuiz(
        this.quizId,
        this.emails.split(",").filter((x) => x.length > 0)
      ).then((res) => {
        if (res.data.isSucceeded) {
          this.$notify({
            title: "Successfully added",
            type: "success",
          });
        } else {
          this.$notify({
            title: "Added fail",
            type: "danger",
          });
        }
      });
    },
    async getQuizVoters(quizId) {
      this.quizId = quizId;
      this.emails = (await QuizService.getQuizVoters(quizId)).data.result.join(
        ", "
      );
    },
  },
};
</script>

<style scoped></style>
