import api from "@/services/api/api";
class UserService {
  test() {
    return api.get("");
  }
}
export default new UserService();
