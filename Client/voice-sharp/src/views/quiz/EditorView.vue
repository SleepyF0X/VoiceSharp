<template>
  <div>
    <div class="row">
      <h1 class="col-10">{{ editorMode ? "Edit" : "Create" }} Quiz</h1>
      <div class="col-2 mt-1">
        <button
          type="button"
          class="btn btn-lg btn-success w-100"
          @click="saveQuiz"
        >
          Save
        </button>
      </div>
    </div>
    <div class="mt-3 w-25">
      <h5>Quiz Name</h5>
      <input
        type="text"
        class="form-control"
        placeholder="Description"
        v-model="model.name"
      />
    </div>
    <div class="mt-3 w-50">
      <h5>Quiz Description</h5>
      <textarea
        class="form-control"
        placeholder="Description"
        style="height: 80px"
        v-model="model.description"
      />
    </div>
    <div class="row">
      <div
        class="col-6 mt-4"
        v-for="(question, index) in model.questions"
        :key="question.id"
      >
        <QuestionEditor :question="question" :index="index" />
      </div>
    </div>
  </div>
  <div class="d-flex justify-content-center mt-3">
    <button type="button" class="btn btn-primary w-25" @click="addQuestion">
      Add question
    </button>
  </div>
</template>

<script>
import QuestionEditor from "@/components/QuestionEditor";
import QuizService from "../../services/quiz/quiz.service";
import { GetPageId } from "@/common/utils";

export default {
  name: "EditorView",
  components: {
    QuestionEditor,
  },
  data() {
    return {
      editorMode: parseInt(GetPageId()),
      model: {
        name: undefined,
        description: undefined,
        questions: [
          {
            name: undefined,
            description: undefined,
            answers: [
              {
                text: undefined,
                value: undefined,
              },
            ],
          },
          {
            name: undefined,
            description: undefined,
            answers: [
              {
                text: undefined,
                value: undefined,
              },
            ],
          },
        ],
      },
    };
  },
  async mounted() {
    if (GetPageId()) {
      this.model = (await QuizService.get(GetPageId())).data.result;
    }
  },
  methods: {
    addQuestion() {
      this.model.questions.push(
        getQuestionBaseModel(this.model.questions.length + 1)
      );
    },
    saveQuiz() {
      this.editorMode
        ? QuizService.edit(GetPageId(), this.model).then((res) => {
            if (res.data.isSucceeded) {
              this.$notify({
                title: "Successfully updated",
                type: "success",
              });
            } else {
              this.$notify({
                title: "Update fail",
                type: "danger",
              });
            }
          })
        : QuizService.createQuiz(this.model).then((res) => {
            if (res.data.isSucceeded) {
              this.$notify({
                title: "Successfully updated",
                type: "success",
                position: "top left",
              });
            } else {
              this.$notify({
                title: "Update fail",
                type: "danger",
              });
            }
          });
    },
    deleteQuestion(index) {
      this.model.questions.splice(index, 1);
    },
  },
};
function getQuestionBaseModel(index) {
  return {
    name: `Question ${index}`,
    description: "Desc",
    answers: [
      {
        text: "txt",
        value: "val",
      },
    ],
  };
}
</script>

<style scoped></style>
