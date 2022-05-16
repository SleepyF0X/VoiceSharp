import api from "@/services/api/api";
class UserService {
  test() {
    return api.get("test");
  }
}
export default new UserService();
