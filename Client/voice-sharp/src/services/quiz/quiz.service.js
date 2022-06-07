import api from "@/services/api/api";
class QuizService {
  getAll() {
    return api.get("quizzes");
  }
  get(quizId) {
    return api.get(`quizzes/${quizId}`);
  }
  getByToken(token) {
    return api.get(`quizzes/passing?token=${token}`);
  }
  getResultsByToken(token) {
    return api.get(`quizzes/results?token=${token}`);
  }
  submitVote(voteModel) {
    return api.post("quizzes/passing", voteModel);
  }
  edit(quizId, quizModel) {
    return api.put(`quizzes/${quizId}/edit`, quizModel);
  }
  delete(quizId) {
    return api.delete(`quizzes/${quizId}/delete`);
  }
  createQuiz(quizModel) {
    return api.post("quizzes/create", quizModel);
  }
  addVotersToQuiz(quizId, emails) {
    return api.post(`quizzes/${quizId}/add-voters`, emails);
  }
  getQuizVoters(quizId) {
    return api.get(`quizzes/${quizId}/voters`);
  }
}
export default new QuizService();
