import TokenService from "@/services/auth/token.service";
import api from "@/services/api/api";
class AuthService {
  login({ username, password }) {
    return api
      .post("/account/login", {
        username,
        password,
      })
      .then((response) => {
        if (response.data.accessToken) {
          TokenService.setUser(response.data);
        }
        return response.data;
      });
  }
  logout() {
    TokenService.removeUser();
  }
  register({ username, email, password }) {
    return api.post("/auth/register", {
      username,
      email,
      password,
    });
  }
}
export default new AuthService();
