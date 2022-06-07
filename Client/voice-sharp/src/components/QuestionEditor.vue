<template>
  <div class="w-100 card">
    <div class="card-header">
      <div class="d-flex justify-content-between">
        <input
          type="text"
          v-model="localQuestion.name"
          class="form-control-lg border-0"
          placeholder="Enter question name"
        />
        <button
          type="button"
          class="btn btn-sm btn-danger"
          @click="$parent.deleteQuestion(index)"
        >
          <i class="fs-4 bi-x"></i>
        </button>
      </div>
    </div>
    <div class="card-body">
      <form>
        <div class="row">
          <div class="col-6">
            <div class="form-group">
              <h5>Description</h5>
              <textarea
                class="form-control"
                placeholder="Description"
                style="height: 80px"
                v-model="localQuestion.description"
              />
            </div>
          </div>
          <div class="col-6">
            <div class="mt-2">
              <h5>Question Type</h5>
              <div class="form-check">
                <input
                  v-model.number="localQuestion.type"
                  class="form-check-input"
                  type="radio"
                  name="exampleRadios"
                  :id="index + 'radio'"
                  :value="1"
                  checked
                />
                <label class="form-check-label" :for="index + 'radio'">
                  Question with one answer
                </label>
              </div>
              <div class="form-check">
                <input
                  v-model.number="localQuestion.type"
                  class="form-check-input"
                  type="radio"
                  name="exampleRadios"
                  :id="index + 'checkbox'"
                  :value="2"
                />
                <label class="form-check-label" :for="index + 'checkbox'">
                  Question with multiple answers
                </label>
              </div>
              <div class="form-check">
                <input
                  v-model.number="localQuestion.type"
                  class="form-check-input"
                  type="radio"
                  name="exampleRadios"
                  :id="index + 'text'"
                  :value="3"
                />
                <label class="form-check-label" :for="index + 'text'">
                  Text input
                </label>
              </div>
            </div>
          </div>
        </div>
        <div>
          <div v-if="question.type !== 3">
            <h5>Answers</h5>
            <div class="row">
              <div
                class="col-6"
                v-for="(answer, aIndex) in localQuestion.answers"
                :key="aIndex"
              >
                <div class="d-flex flex-column card mt-2">
                  <div class="card-body">
                    <div class="row">
                      <div class="col-10">
                        <div class="d-flex align-items-center row">
                          <div class="fs-6 col-3">Text</div>
                          <input
                            v-model="answer.text"
                            type="text"
                            class="form-control ml-1 col-9 w-auto"
                            placeholder="Answer text"
                          />
                        </div>

                        <br />
                        <div class="d-flex align-items-center row">
                          <div class="fs-6 col-3">Value</div>
                          <input
                            v-model="answer.value"
                            type="text"
                            class="form-control ml-1 col-9 w-auto"
                            placeholder="Answer description"
                          />
                        </div>
                      </div>
                      <div class="col-1 d-flex align-items-center">
                        <button
                          type="button"
                          class="btn btn-sm btn-outline-danger"
                          @click="deleteAnswer(aIndex)"
                        >
                          <i class="fs-4 bi-x"></i>
                        </button>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <div class="d-flex justify-content-end">
              <button
                type="button"
                @click="addAnswer"
                class="btn btn-outline-primary mt-3"
              >
                Add answer
              </button>
            </div>
          </div>
        </div>
      </form>
    </div>
  </div>
</template>

<script>
export default {
  name: "QuestionEditor",
  props: {
    question: Object,
    index: Number,
  },
  data() {
    return {
      localQuestion: this.question,
    };
  },
  methods: {
    addAnswer() {
      this.localQuestion.answers.push({
        text: "txt",
        value: "val",
      });
    },
    deleteAnswer(index) {
      this.localQuestion.answers.splice(index, 1);
    },
  },
};
</script>

<style scoped></style>
